using System;
using System.Collections.Generic;

namespace DuPontMirrors
{
	public class Room
	{
		Dictionary<CardinalDirection, bool> isVisited;
		public int X { get; }
		public int Y { get; }
		Mirror mirror;

		public Room (Mirror theMirror, int x, int y)
		{
			X = x;
			Y = y;
			mirror = theMirror;
			isVisited = new Dictionary<CardinalDirection, bool> ();
			isVisited [CardinalDirection.North] = false;
			isVisited [CardinalDirection.South] = false;
			isVisited [CardinalDirection.East] = false;
			isVisited [CardinalDirection.West] = false;
		}

		/**
		 * Works as a factory method for mirrors. Given the type,
		 * returns the appropriate mirror.
		 * 
		 * @see Mirror for more information on the mirror class.
		 * @see WestLeaningDoubleMirror for info on leaning double mirrors.
		 * @see NorthEastFacingMirror for info on directional facing mirrors
		 */
		public static Mirror GetMirrorFromType(string type) {
			switch (type) {
			case "L": 
				return new WestLeaningDoubleMirror ();
			case "R": 
				return new EastLeaningDoubleMirror ();
			case "RL": 
				return new NorthWestFacingMirror ();
			case "RR":
				return new SouthEastFacingMirror ();
			case "LR":
				return new NorthEastFacingMirror ();
			case "LL":
				return new SouthWestFacingMirror ();
			default:
				return null;
			}
		}
			
		public void Visit(CardinalDirection direction)
		{
			isVisited [direction] = true;
		}

		public bool WasVisited (CardinalDirection direction)
		{
			return isVisited [direction];
		}

		public bool HasReflection (CardinalDirection direction)
		{
			return mirror.HasReflection (direction);
		}

		public CardinalDirection GetReflectedDirection (CardinalDirection direction)
		{
			return mirror.ReflectedDirection (direction);
		}
	}
}
