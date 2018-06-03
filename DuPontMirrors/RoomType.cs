using System;

namespace DuPontMirrors
{
	// TODO: Check nomenclature here
	public enum RoomType
	{
		None = 0,
		TwoWayMirrorLeansWest = 1,  // "L"
		TwoWayMirrorLeansEast = 2, // "R"
		OneWayMirrorLeansEastReflectsNorth = 3, // "RL"
		OneWayMirrorLeansEastReflectsSouth = 4, // "RR"
		OneWayMirrorLeansWestReflectsNorth = 5, // "LR"
		OneWayMirrorLeansWestReflectsSouth = 6 // "LL"
	}
}

