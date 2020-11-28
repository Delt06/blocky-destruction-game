using System;
using System.Collections.Generic;
using UnityEngine;

public enum WorldSide
{
	North,
	South,
	East,
	West,
	Top,
	Bottom
}

public static class WorldSides
{
	public static IReadOnlyList<WorldSide> All { get; } = (WorldSide[]) Enum.GetValues(typeof(WorldSide));

	public static Vector3Int ToVector(this WorldSide worldSide)
	{
		switch (worldSide)
		{
			case WorldSide.North:
				return new Vector3Int(0, 0, 1);
			case WorldSide.South:
				return new Vector3Int(0, 0, -1);
			case WorldSide.East:
				return new Vector3Int(1, 0, 0);
			case WorldSide.West:
				return new Vector3Int(-1, 0, 0);
			case WorldSide.Top:
				return new Vector3Int(0, 1, 0);
			case WorldSide.Bottom:
				return new Vector3Int(0, -1, 0);
			default:
				throw new ArgumentOutOfRangeException(nameof(worldSide), worldSide, null);
		}
	}
}