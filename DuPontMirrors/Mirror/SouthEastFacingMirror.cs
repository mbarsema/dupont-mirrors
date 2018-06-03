using System;

namespace DuPontMirrors
{
	public class SouthEastFacingMirror : Mirror
	{
		public SouthEastFacingMirror () : base()
		{
			IsTwoWayMirror = false;
			Symbol = "RR";
		}

		protected override void SetCanReflect() {
			CanReflect [CardinalDirection.North] = true;
			CanReflect [CardinalDirection.West] = true;
		}

		protected override void SetReflectedDirection() {
			Reflected [CardinalDirection.North] = CardinalDirection.East;
			Reflected [CardinalDirection.West] = CardinalDirection.South;
		}
	}
}

