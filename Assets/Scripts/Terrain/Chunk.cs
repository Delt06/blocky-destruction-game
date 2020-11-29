using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Terrain
{
	public sealed class Chunk : MonoBehaviour
	{
		[SerializeField, Min(1)] private int _sizeX = 16;
		[SerializeField, Min(1)] private int _sizeY = 16;
		[SerializeField, Min(1)] private int _sizeZ = 16;

		public void SetSphere(Vector3 worldCenter, float radius, [NotNull] IBlock block)
		{
			if (block == null) throw new ArgumentNullException(nameof(block));

			var modified = 0;

			for (var x = -radius; x <= radius; x++)
			{
				for (var y = -radius; y <= radius; y++)
				{
					for (var z = -radius; z <= radius; z++)
					{
						var offset = new Vector3(x, y, z);
						if (offset.magnitude > radius) continue;

						var position = worldCenter + offset;
						if (!TryConvertToBlockIndex(position, out var blockIndex)) continue;
						if (Blocks[blockIndex.x, blockIndex.y, blockIndex.z] == block) continue;

						Blocks[blockIndex.x, blockIndex.y, blockIndex.z] = block;
						modified++;
					}
				}
			}

			if (modified > 0)
				Modified?.Invoke(this, EventArgs.Empty);
		}

		public bool TryConvertToBlockIndex(Vector3 position, out Vector3Int blockIndex)
		{
			var origin = transform.position;
			position -= origin;
			blockIndex = VectorConversion.Floor(position);
			return 0 <= blockIndex.x && blockIndex.x < SizeX &&
			       0 <= blockIndex.y && blockIndex.y < SizeY &&
			       0 <= blockIndex.z && blockIndex.z < SizeZ;
		}

		public bool ContainsWorldPosition(Vector3 position)
		{
			var center = transform.position + (Vector3) Size / 2f;
			var bounds = new Bounds(center, Size);
			return bounds.Contains(position);
		}

		public int SizeX => _sizeX;
		public int SizeY => _sizeY;

		public int SizeZ => _sizeZ;

		public Vector3Int Size => new Vector3Int(SizeX, SizeY, SizeZ);

		public IBlock this[int x, int y, int z]
		{
			get
			{
				CheckRange(x, y, z);
				return Blocks[x, y, z];
			}

			set
			{
				CheckRange(x, y, z);
				if (Blocks[x, y, z] == value) return;
				Blocks[x, y, z] = value ?? throw new ArgumentNullException(nameof(value));
				Modified?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler Modified;

		public IBlock this[Vector3Int position]
		{
			get => this[position.x, position.y, position.z];
			set => this[position.x, position.y, position.z] = value ?? throw new ArgumentNullException(nameof(value));
		}

		private IBlock[,,] Blocks { get; set; }

		private void CheckRange(int x, int y, int z)
		{
			if (x < 0 || x >= SizeX) throw new ArgumentOutOfRangeException(nameof(x));
			if (y < 0 || x >= SizeY) throw new ArgumentOutOfRangeException(nameof(y));
			if (z < 0 || x >= SizeZ) throw new ArgumentOutOfRangeException(nameof(z));
		}

		private void Awake()
		{
			var generator = GetComponent<BlockTerrainGenerator>();
			Blocks = generator.Generate(SizeX, SizeY, SizeZ);
		}
	}
}