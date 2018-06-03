using System;
using System.Collections.Generic;

namespace DuPontMirrors
{
	public class LightBeam
	{
		public Vector InitialVector { get; }
		public Vector CurrentVector { get; private set; }
		public List<Vector> Trail { get; }

		public LightBeam (Vector initial)
		{
			this.InitialVector = new Vector (initial.X, initial.Y, initial.Direction);
			this.CurrentVector = new Vector (initial.X, initial.Y, initial.Direction);
			this.Trail = new List<Vector> ();
			this.Trail.Add (this.CurrentVector);
		}

		public void Traverse()
		{
			this.CurrentVector = Vector.Increment(this.CurrentVector);
			this.Trail.Add (this.CurrentVector);
		}

		public void Bend(CardinalDirection direction)
		{
			this.CurrentVector = Vector.Redirect(this.CurrentVector, direction);
			this.Trail.Add (this.CurrentVector);
			this.Traverse ();
		}
	}
}

