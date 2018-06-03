using System;
using System.IO;

namespace DuPontMirrors
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string path = PromptUserForFile();
			Building building;
			LightBeam beam;

			try {
				building = new Building (path);
			} catch (Exception) {
				Console.WriteLine ("Error: unable to create board");
				Console.WriteLine ("Press any key to continue");
				Console.ReadKey ();
				return;
			}

			try {
				beam = new LightBeam (building.InitialVector);
			} catch (Exception) {
				Console.WriteLine ("Error: unable to create light source");
				Console.WriteLine ("Press any key to continue");
				Console.ReadKey ();
				return;
			}

			while (building.Contains(beam) && !building.WasVisited(beam)) {
				if (building.HasReflection (beam)) {
					building.Reflect (beam);
				} else {
					beam.Traverse ();
				}
			}

			PrintResults (building, beam);

			Console.WriteLine ("Press any key to continue");
			Console.ReadKey ();
		}

		public static string PromptUserForFile()
		{
			Console.WriteLine ("Enter path to file:");
			string file = Console.ReadLine ();

			while (!File.Exists (file)) {
				Console.WriteLine ("File does not exist");
				Console.WriteLine ("Enter path to file:");
				file = Console.ReadLine ();
			}

			return file;
		}

		public static void PrintResults(Building building, LightBeam beam)
		{
			Console.WriteLine ("Board dimensions: {0} (w) x {1} (h)", building.Width, building.Height);
			Console.WriteLine (
				"Initial starting position {0},{1}\t{2}",
				building.InitialVector.X,
				building.InitialVector.Y,
				building.InitialVector.Direction == CardinalDirection.North || building.InitialVector.Direction == CardinalDirection.South ? "V" : "H"
			);

			if (!building.Contains (beam)) {
				Vector final = beam.Trail [beam.Trail.Count - 2];

				Console.WriteLine (
					"Exit position {0},{1}\t{2}",
					final.X,
					final.Y,
					final.Direction == CardinalDirection.North || final.Direction == CardinalDirection.South ? "V" : "H"
				);
			} else {
				Console.WriteLine ("Beam entered a loop and never exited");
				Console.WriteLine ("Last position before loop detected:");

				Vector final = beam.Trail [beam.Trail.Count - 2];

				Console.WriteLine (
					"Exit position {0},{1}\t{2}",
					final.X,
					final.Y,
					final.Direction == CardinalDirection.North || final.Direction == CardinalDirection.South ? "V" : "H"
				);
			}

			Console.WriteLine ("Beam trail:");
			foreach (Vector v in beam.Trail) {
				if (v.X >= 0 && v.Y >= 0 && v.X < building.Width && v.Y < building.Height) {
					Console.WriteLine (string.Format ("{0},{1}\t Heading {2}", v.X, v.Y, v.Direction));
				}
			}
		}
	}
}
