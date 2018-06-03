using System;
using System.Collections.Generic;

namespace DuPontMirrors
{
	abstract public class Mirror
	{
		public string Symbol { get; protected set; }
		public bool IsTwoWayMirror { get; protected set; }
		protected Dictionary<CardinalDirection, bool> CanReflect { get; }
		protected Dictionary<CardinalDirection, CardinalDirection> Reflected { get; }
		protected CardinalDirection Faces { get; }

		public Mirror () {
			Symbol = "M";
			IsTwoWayMirror = false;
			CanReflect = new Dictionary<CardinalDirection, bool> ();
			Reflected = new Dictionary<CardinalDirection, CardinalDirection> ();

			CanReflect [CardinalDirection.North] = false;
			CanReflect [CardinalDirection.South] = false;
			CanReflect [CardinalDirection.East] = false;
			CanReflect [CardinalDirection.West] = false;

			Reflected [CardinalDirection.North] = CardinalDirection.None;
			Reflected [CardinalDirection.South] = CardinalDirection.None;
			Reflected [CardinalDirection.East] = CardinalDirection.None;
			Reflected [CardinalDirection.West] = CardinalDirection.None;

			SetCanReflect ();
			SetReflectedDirection ();
		}

		public bool HasReflection (CardinalDirection direction) {
			return CanReflect[direction];
		}

		public CardinalDirection ReflectedDirection (CardinalDirection direction) {
			return Reflected [direction];
		}

		abstract protected void SetCanReflect();
		abstract protected void SetReflectedDirection();
	}
}

