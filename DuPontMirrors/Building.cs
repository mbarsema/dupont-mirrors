using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DuPontMirrors
{
	public class Building
	{
		Room[,] rooms;
		public int Height { get; private set; }
		public int Width { get; private set; }
		public Vector InitialVector { get; private set; }

		/**
		 * Constructor for Building.
		 * Takes a string based file path and then derives the building in three phases.
		 * Phase 1 defines the actual building.
		 * Phase 2 defines the rooms in the building
		 * Phase 3 defines the initial vector the light will travel.
		 */
		public Building (string path) {
			string line;
			StreamReader file = new System.IO.StreamReader(path);

			int phase = 0;
			while((line = file.ReadLine()) != null)
			{
				if (line == "-1") {
					phase++;
					continue;
				}

				switch (phase) {
					case 0:
						SetBuildingSize (line);
						break;
					case 1:
						CreateRoom (line);
						break;
					case 2:
						SetInitialVector (line);
						break;
				}
			}

			file.Close();
		}

		/**
		 * Constructor for building
		 * Takes a width and height as parameters.
		 */
		public Building (int width, int height)
		{
			SetBuildingDimensions (width, height);
		}

		/**
		 * Parses a line for the defined coordinates from phase 1.
		 * Then converts those coordinates to an integer and then passes them
		 * to creating building dimensions.
		 */
		private void SetBuildingSize (string line) {
			string [] coordinates = line.Split (',');
			SetBuildingDimensions (
				Convert.ToInt32(coordinates[0]),
				Convert.ToInt32(coordinates[1])
			);
		}

		private void SetBuildingDimensions(int width, int height) {
			Width = width;
			Height = height;
			rooms = new Room[width, height]; // TODO: This needs to have better memory management
		}

		/**
		 * Creates a room given the format "1,2LR"
		 */
		private void CreateRoom(string line) {
			string [] coordinates = line.Split(',');

			// This pattern matches the format L, R, LL, LR, RL, RR
			string mirrorType = ParseCoordinateMetaData(coordinates[1], "[L|R]{1,2}");
			int x = Convert.ToInt32 (coordinates[0]);
			int y = ParseCoordinate(coordinates[1]);

			AddRoom (x, y, mirrorType);
		}

		/**
		 * Sets the initial vector given the format "0,1V"
		 */
		private void SetInitialVector (string line) {
			string[] coordinates = line.Split (',');
			int x = Convert.ToInt32 (coordinates [0]);
			int y = ParseCoordinate (coordinates [1]);

			// This pattern matches the format "V" (vertical) or "H" (horizontal)
			CardinalDirection direction = GetInitialDirection(
				x,
				y, 
				ParseCoordinateMetaData (coordinates [1], "[V|H]")
			);

			InitialVector = new Vector (x, y, direction);
		}

		/**
		 * Parses a string in the format "12RR" where "RR" can be a form of meta-data and returns
		 * only the coordinate (in this case 12).
		 */
		public static int ParseCoordinate(string coordinate) {
			return Convert.ToInt32 (Regex.Match (coordinate, @"\d+").Value);
		}

		/**
		 * Parses a string in the format "12RR" where "RR" is some form of meta-data and
		 * returns only the meta-data (in this case "RR" which denotes a type of mirror).
		 */
		public static string ParseCoordinateMetaData(string coordinate, string pattern) {
			return Regex.Match (coordinate.ToUpper (), pattern).Value;
		}

		/**
		 * Converts a string and a pair of coordinates into the initial cardinal direction.
		 * If we start at 0, we know that it's the bottom and we want to head north (can't go south)
		 * or east (can't go west). If it's non-zero we want to go in the opposite direction.
		 */
		public static CardinalDirection GetInitialDirection(int x, int y, string stringlyDirection) {
			CardinalDirection direction;

			if (stringlyDirection == "V") {
				direction = y == 0 ? CardinalDirection.North : CardinalDirection.South;
			} else if (stringlyDirection == "H") {
				direction = x == 0 ? CardinalDirection.East : CardinalDirection.West;
			} else {
				throw new InvalidDataException ("Invalid initial vector");
			}

			return direction;
		}


		public void AddRoom (int x, int y, string mirrorType)
		{
			AddRoom(x, y, Room.GetMirrorFromType(mirrorType));
		}

		public void AddRoom (int x, int y, Mirror mirror)
		{
			rooms [x, y] = new Room (mirror, x, y);
		}

		public bool Contains(LightBeam beam)
		{
			return beam.CurrentVector.X >= 0 && beam.CurrentVector.X < this.Width && 
				beam.CurrentVector.Y >= 0  && beam.CurrentVector.Y < this.Height;
		}

		public Room GetCurrentRoom(Vector vector)
		{
			return this.rooms[vector.X, vector.Y];
		}

		public void MarkRoomAsVisited(Room room, Vector vector)
		{
			room.Visit (vector.Direction);
		}

		public bool WasVisited(LightBeam beam)
		{
			Room room = GetCurrentRoom (beam.CurrentVector);
			return room == null ? false : room.WasVisited (beam.CurrentVector.Direction);
		}

		public bool HasReflection(LightBeam beam)
		{
			Room room = GetCurrentRoom (beam.CurrentVector);
			return room != null && room.HasReflection (beam.CurrentVector.Direction);
		}

		public CardinalDirection GetUpdatedDirection(Room room, Vector vector)
		{
			return room.GetReflectedDirection(vector.Direction);
		}

		public void Reflect(LightBeam beam)
		{
			Room room = this.GetCurrentRoom (beam.CurrentVector);
			Vector vector = beam.CurrentVector;
			beam.Bend (this.GetUpdatedDirection (room, beam.CurrentVector));
			MarkRoomAsVisited (room, vector);
		}
	}
}
