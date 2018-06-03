using System;

namespace DuPontMirrors
{
	/**
	 * This is a single mirror that faces north east 
	 * (or "LR" for "Leans left with the right reflecting"). 
	 * This means that it will reflect light coming from the opposite directions only.
	 */
	public class NorthEastFacingMirror : Mirror
	{
		public NorthEastFacingMirror () : base()
		{
			IsTwoWayMirror = false;
			Symbol = "LR";
		}

		protected override void SetCanReflect() {
			CanReflect [CardinalDirection.South] = true;
			CanReflect [CardinalDirection.West] = true;
		}

		protected override void SetReflectedDirection() {
			Reflected [CardinalDirection.South] = CardinalDirection.East;
			Reflected [CardinalDirection.West] = CardinalDirection.North;
		}
	}
}

