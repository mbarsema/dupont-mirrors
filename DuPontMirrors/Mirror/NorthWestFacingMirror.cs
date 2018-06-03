using System;

namespace DuPontMirrors
{
	public class NorthWestFacingMirror : Mirror
	{
		public NorthWestFacingMirror () : base()
		{
			IsTwoWayMirror = false;
			Symbol = "RL";
		}

		protected override void SetCanReflect() {
			CanReflect [CardinalDirection.South] = true;
			CanReflect [CardinalDirection.East] = true;
		}

		protected override void SetReflectedDirection() {
			Reflected [CardinalDirection.South] = CardinalDirection.West;
			Reflected [CardinalDirection.East] = CardinalDirection.North;
		}
	}
}

