using System;

namespace DuPontMirrors
{
	/**
	 * This is a double mirror that leans East (or "R" for "Right"). 
	 * A double mirror means one that can reflect on both sides.
	 * "Leaning" in this case means that the angle of reflection is
	 * 90 degrees.
	 */
	public class EastLeaningDoubleMirror : Mirror
	{
		public EastLeaningDoubleMirror () : base()
		{
			IsTwoWayMirror = true;
			Symbol = "R";
		}

		protected override void SetCanReflect() {
			CanReflect [CardinalDirection.North] = true;
			CanReflect [CardinalDirection.South] = true;
			CanReflect [CardinalDirection.East] = true;
			CanReflect [CardinalDirection.West] = true;
		}

		protected override void SetReflectedDirection() {
			Reflected [CardinalDirection.South] = CardinalDirection.West;
			Reflected [CardinalDirection.North] = CardinalDirection.East;
			Reflected [CardinalDirection.East] = CardinalDirection.North;
			Reflected [CardinalDirection.West] = CardinalDirection.South;
		}
	}
}

