using System;

namespace DuPontMirrors
{
	public class Vector
	{
		public int X { get; }
		public int Y { get; }
		public int Length { get; }
		public CardinalDirection Direction { get; }
				
		public Vector (int x, int y, CardinalDirection direction, int length = 1)
		{
			this.X = x;
			this.Y = y;
			this.Direction = direction;
			this.Length = length;
		}

		public static Vector Increment (Vector vector)
		{
			switch (vector.Direction) {
				case CardinalDirection.North:
					return new Vector(vector.X, vector.Y + vector.Length, CardinalDirection.North);
				case CardinalDirection.South:
					return new Vector(vector.X, vector.Y - vector.Length, CardinalDirection.South);
				case CardinalDirection.East:
					return new Vector(vector.X + vector.Length, vector.Y, CardinalDirection.East);
				case CardinalDirection.West:
					return new Vector(vector.X - vector.Length, vector.Y, CardinalDirection.West);
			}

			return vector;
		}

		public static Vector Redirect (Vector vector, CardinalDirection direction) 
		{
			return new Vector(vector.X, vector.Y, direction); 
		}
	}
}

