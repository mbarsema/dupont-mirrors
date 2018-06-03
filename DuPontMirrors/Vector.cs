using System;

namespace DuPontMirrors
{
	/**
	 * For the purposes of this exercise this is a vector in terms of mathematical terminology, NOT
	 * in terms of computer science terminology. It also only applies to the X and Y axes.
	 */
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
			// This allows us to multiply the length by the modifier to add or subtract the correct value
			int modifier = Convert.ToInt32(vector.Direction);

			// The x-axis is doubled in the enum, so we can divide it by 2 to get the appropriate modifier.
			if (vector.Direction == CardinalDirection.East || vector.Direction == CardinalDirection.West) {
				modifier /= 2;
			}

			return new Vector (
				TransformX(vector.X, vector.Length, modifier, vector.Direction),
				TransformY(vector.Y, vector.Length, modifier, vector.Direction),
				vector.Direction
			);
		}

		protected static int TransformX (int x, int length, int modifier, CardinalDirection direction) {
			return direction == CardinalDirection.East || direction == CardinalDirection.West
				? TransformValue (x, length, modifier) : x;
		}

		protected static int TransformY (int y, int length, int modifier, CardinalDirection direction) {
			return direction == CardinalDirection.North || direction == CardinalDirection.South
				? TransformValue (y, length, modifier) : y;
		}

		protected static int TransformValue(int value, int length, int modifier) {
			return value + length * modifier;
		}
			
		public static Vector Redirect (Vector vector, CardinalDirection direction) 
		{
			return new Vector(vector.X, vector.Y, direction); 
		}
	}
}

