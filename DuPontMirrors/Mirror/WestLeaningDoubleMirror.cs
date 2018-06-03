using System;

namespace DuPontMirrors
{
	public class WestLeaningDoubleMirror : Mirror
	{
		/**
	     * This is a double mirror that leans West (or "L" for "Left"). 
	     * A double mirror means one that can reflect on both sides.
	     * "Leaning" in this case means that the angle of reflection is
	     * 90 degrees.
	     */
		public WestLeaningDoubleMirror () : base()
		{
			IsTwoWayMirror = true;
			Symbol = "L";
		}

		protected override void SetCanReflect() {
			CanReflect [CardinalDirection.North] = true;
			CanReflect [CardinalDirection.South] = true;
			CanReflect [CardinalDirection.East] = true;
			CanReflect [CardinalDirection.West] = true;
		}

		protected override void SetReflectedDirection() {
			Reflected [CardinalDirection.South] = CardinalDirection.East;
			Reflected [CardinalDirection.North] = CardinalDirection.West;
			Reflected [CardinalDirection.East] = CardinalDirection.South;
			Reflected [CardinalDirection.West] = CardinalDirection.North;
		}
	}
}

