using System;

namespace DuPontMirrors
{
	public class EastFacingMirror : Mirror
	{
		public EastFacingMirror () : base(true, CardinalDirection.East)
		{
			Symbol = "R";
		}

		new protected void SetCanReflectTwoWayMirror() {
			CanReflect [CardinalDirection.North] = true;
			CanReflect [CardinalDirection.South] = true;
			CanReflect [CardinalDirection.East] = true;
			CanReflect [CardinalDirection.West] = true;
		}

		new protected void SetReflectedDirectionTwoWayMirror() {
			Reflected [CardinalDirection.South] = CardinalDirection.West;
			Reflected [CardinalDirection.North] = CardinalDirection.East;
			Reflected [CardinalDirection.East] = CardinalDirection.North;
			Reflected [CardinalDirection.West] = CardinalDirection.South;
		}
	}
}

