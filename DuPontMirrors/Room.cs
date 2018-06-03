using System;
using System.Collections.Generic;

namespace DuPontMirrors
{
	public class Room
	{
		Dictionary<CardinalDirection, bool> isVisited;
		Dictionary<CardinalDirection, bool> canReflect;
		Dictionary<CardinalDirection, CardinalDirection> reflectedDirection;
		RoomType roomType;
		bool leansEast;
		bool leansWest;

		public Room (RoomType type, int startX, int startY)
		{
			roomType = type;
			leansEast = false;
			leansWest = false;

			isVisited = new Dictionary<CardinalDirection, bool> ();
			isVisited [CardinalDirection.North] = false;
			isVisited [CardinalDirection.South] = false;
			isVisited [CardinalDirection.East] = false;
			isVisited [CardinalDirection.West] = false;

			canReflect = new Dictionary<CardinalDirection, bool> ();
			canReflect [CardinalDirection.North] = false;
			canReflect [CardinalDirection.South] = false;
			canReflect [CardinalDirection.East] = false;
			canReflect [CardinalDirection.West] = false;

			reflectedDirection = new Dictionary<CardinalDirection, CardinalDirection> ();

			if (type == RoomType.TwoWayMirrorLeansEast || type == RoomType.OneWayMirrorLeansEastReflectsNorth || type == RoomType.OneWayMirrorLeansEastReflectsSouth) {
				leansEast = true;
			}

			if (type == RoomType.TwoWayMirrorLeansWest || type == RoomType.OneWayMirrorLeansWestReflectsNorth || type == RoomType.OneWayMirrorLeansWestReflectsSouth) {
				leansWest = true;
			}

			if (type == RoomType.TwoWayMirrorLeansEast || type == RoomType.TwoWayMirrorLeansWest) {
				canReflect [CardinalDirection.North] = true;
				canReflect [CardinalDirection.South] = true;
				canReflect [CardinalDirection.East] = true;
				canReflect [CardinalDirection.West] = true;
			} else {
				if (roomType == RoomType.OneWayMirrorLeansEastReflectsSouth || roomType == RoomType.OneWayMirrorLeansWestReflectsSouth) {
					canReflect [CardinalDirection.North] = true;
				}

				if (roomType == RoomType.OneWayMirrorLeansEastReflectsNorth || roomType == RoomType.OneWayMirrorLeansWestReflectsNorth) {
					canReflect [CardinalDirection.South] = true;
				}

				if (roomType == RoomType.OneWayMirrorLeansWestReflectsNorth || roomType == RoomType.OneWayMirrorLeansEastReflectsSouth) {
					canReflect [CardinalDirection.East] = true;
				}

				if (roomType == RoomType.OneWayMirrorLeansEastReflectsNorth || roomType == RoomType.OneWayMirrorLeansWestReflectsSouth) {
					canReflect [CardinalDirection.West] = true;
				}
			}

			if (leansEast) {
				reflectedDirection [CardinalDirection.North] = CardinalDirection.East;
				reflectedDirection [CardinalDirection.South] = CardinalDirection.West;
				reflectedDirection [CardinalDirection.East] = CardinalDirection.North;
				reflectedDirection [CardinalDirection.West] = CardinalDirection.South;
			} else if (leansWest) {
				reflectedDirection [CardinalDirection.North] = CardinalDirection.West;
				reflectedDirection [CardinalDirection.South] = CardinalDirection.East;
				reflectedDirection [CardinalDirection.East] = CardinalDirection.South;
				reflectedDirection [CardinalDirection.West] = CardinalDirection.North;
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
			return canReflect [direction];
		}

		public CardinalDirection GetReflectedDirection (CardinalDirection direction)
		{
			return reflectedDirection [direction];
		}
	}
}
