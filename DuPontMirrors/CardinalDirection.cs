using System;

namespace DuPontMirrors
{
	/**
	 * Cardinal Directions
	 * We set south and west as negative because on an Cartesian plane they are the negative direction
	 * We set east and west as +/- 2 so that we can divide by 2 to get the appropriate value to increment/decrement
	 * This adds an incredibly minor performance boost when incrementing our vector.
	 */
	public enum CardinalDirection
	{
		West = -2,
		South = -1,
		None = 0,
		North = 1,
		East = 2 
	}
}

