using System;

namespace DuPontMirrors
{
	public class WestFacingMirror : Mirror
	{
		public WestFacingMirror () : base(true, CardinalDirection.West)
		{
			Symbol = "L";
		}

		new protected void SetCanReflectTwoWayMirror() {
			CanReflect [CardinalDirection.North] = true;
			CanReflect [CardinalDirection.South] = true;
			CanReflect [CardinalDirection.East] = true;
			CanReflect [CardinalDirection.West] = true;
		}

		new protected void SetReflectedDirectionTwoWayMirror() {
			Reflected [CardinalDirection.South] = CardinalDirection.East;
			Reflected [CardinalDirection.North] = CardinalDirection.West;
			Reflected [CardinalDirection.East] = CardinalDirection.South;
			Reflected [CardinalDirection.West] = CardinalDirection.North;
		}
	}
}

