using UnityEngine;

namespace Terrain
{
	public sealed class World : MonoBehaviour
	{
		[SerializeField] private Chunk _chunkPrefab = default;
		[SerializeField, Min(1)] private int _chunksX = 1;
		[SerializeField, Min(1)] private int _chunksZ = 1;
		[SerializeField] private Vector3 _offset = Vector3.zero;

		private void Awake()
		{
			_chunks = new Chunk[_chunksX, _chunksZ];

			for (var xi = 0; xi < _chunksX; xi++)
			{
				for (var zi = 0; zi < _chunksZ; zi++)
				{
					var position = transform.position;
					position += new Vector3(xi * _chunkPrefab.SizeX, 0, zi * _chunkPrefab.SizeZ);
					position += _offset;
					_chunks[xi, zi] = Instantiate(_chunkPrefab, position, Quaternion.identity, transform);
				}
			}
		}

		private Chunk[,] _chunks;

		private void OnDrawGizmos()
		{
			if (Application.isPlaying) return;
			if (_chunkPrefab == null) return;

			Gizmos.color = Color.green;
			var size = new Vector3(_chunksX * _chunkPrefab.SizeX, 0, _chunksZ * _chunkPrefab.SizeZ);
			Gizmos.DrawCube(transform.position + _offset + size / 2f, size);
		}
	}
}