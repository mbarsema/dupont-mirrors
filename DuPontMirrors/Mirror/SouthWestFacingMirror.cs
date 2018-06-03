using System;

namespace DuPontMirrors
{
	public class SouthWestFacingMirror : Mirror
	{
		public SouthWestFacingMirror () : base()
		{
			IsTwoWayMirror = false;
			Symbol = "LL";
		}

		protected override void SetCanReflect() {
			CanReflect [CardinalDirection.North] = true;
			CanReflect [CardinalDirection.East] = true;
		}

		protected override void SetReflectedDirection() {
			Reflected [CardinalDirection.North] = CardinalDirection.West;
			Reflected [CardinalDirection.East] = CardinalDirection.South;
		}
	}
}

