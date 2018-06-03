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

				if (phase == 0) {
					SetBuildingSize (line);
				}

				if (phase == 1) {
					CreateRoom (line);
				}

				if (phase == 2) {
					SetInitialVector (line);
				}
			}

			file.Close();
		}

		private void SetBuildingSize (string line) {
			string [] coordinates = line.Split (',');
			Width = Convert.ToInt32 (coordinates [0]);
			Height = Convert.ToInt32 (coordinates [1]);
			rooms = new Room[Width, Height]; // TODO: This needs to have better memory management
		}

		private void CreateRoom(string line) {
			string [] coordinates = line.Split(',');
			int x = Convert.ToInt32 (coordinates [0]);
			int y = ParseCoordinate(coordinates[1]);
			string mirrorType = ParseCoordinateMetaData(coordinates[1], "[L|R]{1,2}");
			AddRoom (x, y, mirrorType);
		}

		private void SetInitialVector (string line) {
			string[] coordinates = line.Split (',');
			int x = Convert.ToInt32 (coordinates [0]);
			int y = ParseCoordinate (coordinates [1]);
			CardinalDirection direction = GetCardinalDirection(x, y, ParseCoordinateMetaData (coordinates [1], "[V|H]"));

			InitialVector = new Vector (x, y, direction);
		}

		public static int ParseCoordinate(string coordinate) {
			return Convert.ToInt32 (Regex.Match (coordinate, @"\d+").Value);
		}

		public static string ParseCoordinateMetaData(string coordinate, string pattern) {
			return Regex.Match (coordinate.ToUpper (), pattern).Value;
		}

		public static CardinalDirection GetCardinalDirection(int x, int y, string stringlyDirection) {
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
			
		public Building (int width, int height)
		{
			this.Width = width;
			this.Height = height;
			rooms = new Room[width, height];
		}

		public static RoomType ConvertStringToRoomType (string type) {
			switch (type) {
				case "L":
					return RoomType.TwoWayMirrorLeansWest;
				case "R":
					return RoomType.TwoWayMirrorLeansEast;
				case "RL":
					return RoomType.OneWayMirrorLeansEastReflectsNorth;
				case "RR":
					return RoomType.OneWayMirrorLeansEastReflectsSouth;
				case "LR":
					return RoomType.OneWayMirrorLeansWestReflectsNorth;
				case "LL":
					return RoomType.OneWayMirrorLeansWestReflectsSouth;
				default:
					return RoomType.None;
			}
		}

		public void AddRoom (int x, int y, string mirrorType)
		{
			RoomType type = ConvertStringToRoomType (mirrorType);
			rooms[x, y] = new Room (type, x, y);
		}

		public void AddRoom (int x, int y, RoomType type)
		{
			rooms [x, y] = new Room (type, x, y);
		}

		public bool Contains(LightBeam beam)
		{
			return beam.CurrentVector.X >= 0 && beam.CurrentVector.Y >= 0 && beam.CurrentVector.X < this.Width && beam.CurrentVector.Y < this.Height;
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
