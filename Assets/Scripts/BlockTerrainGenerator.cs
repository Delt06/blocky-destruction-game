using UnityEngine;

public sealed class BlockTerrainGenerator : MonoBehaviour
{
	[SerializeField] private BlockData _dirt = default;
	[SerializeField] private BlockData _grass = default;
	[SerializeField] private Vector3 _scale = Vector3.one;

	public IBlock[,,] Generate(int sizeX, int sizeY, int sizeZ)
	{
		var origin = transform.position;
		var originX = Mathf.RoundToInt(origin.x);
		var originZ = Mathf.RoundToInt(origin.z);
		var blocks = new IBlock[sizeX, sizeY, sizeZ];

		for (var x = 0; x < sizeX; x++)
		{
			for (var z = 0; z < sizeZ; z++)
			{
				var yTop = GetY(x + originX, z + originZ);
				if (yTop >= sizeY)
					yTop = sizeY - 1;

				var y = 0;

				for (; y < yTop; y++)
				{
					blocks[x, y, z] = _dirt;
				}

				blocks[x, y, z] = _grass;
				y++;

				for (; y < sizeY; y++)
				{
					blocks[x, y, z] = NullBlock.Instance;
				}
			}
		}

		return blocks;
	}

	private int GetY(int x, int z)
	{
		var noiseValue = Mathf.PerlinNoise(x * _scale.x, z * _scale.z);
		return Mathf.RoundToInt(noiseValue * _scale.y);
	}
}