using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshCollider))]
public sealed class BlockTerrainMesh : MonoBehaviour
{
	[SerializeField, Min(0f)] private float _borderInPixels = 0.1f;

	private void Start()
	{
		Refresh();
	}

	private void OnEnable()
	{
		_chunk.Modified += _onModified;
	}

	private void OnDisable()
	{
		_chunk.Modified -= _onModified;
	}

	private void Refresh()
	{
		_meshBuilder.Clear();

		var size = _chunk.Size;

		for (var x = 0; x < size.x; x++)
		{
			for (var y = 0; y < size.y; y++)
			{
				for (var z = 0; z < size.z; z++)
				{
					var position = new Vector3Int(x, y, z);
					BuildBlock(position, size);
				}
			}
		}

		var mesh = _meshBuilder.Build();
		_meshCollider.sharedMesh = mesh;
	}

	private void BuildBlock(Vector3Int position, Vector3Int size)
	{
		var block = _chunk[position];
		if (!block.IsVisible) return;
		_meshBuilder.AtPosition(position).WithBlock(block);

		foreach (var side in WorldSides.All)
		{
			if (IsNeighborVisible(position, side, size)) continue;
			_meshBuilder.AddSide(side);
		}
	}

	private bool IsNeighborVisible(Vector3Int position, WorldSide side, Vector3Int size)
	{
		var neighborPosition = position + side.ToVector();
		if (OutOfBounds(neighborPosition, size)) return false;
		var neighbor = _chunk[neighborPosition];
		return neighbor.IsVisible;
	}

	private static bool OutOfBounds(Vector3Int position, Vector3Int size) =>
		position.x < 0 || position.x >= size.x ||
		position.y < 0 || position.y >= size.y ||
		position.z < 0 || position.z >= size.z;

	private void Awake()
	{
		_chunk = GetComponent<Chunk>();
		_meshFilter = GetComponent<MeshFilter>();
		var mesh = new Mesh();
		_meshFilter.sharedMesh = mesh;
		_meshBuilder = new BlockTerrainMeshBuilder(mesh, _borderInPixels);
		_meshCollider = GetComponent<MeshCollider>();
		_meshFilter.sharedMesh = mesh;
		_onModified = (sender, args) => Refresh();
	}

	private Chunk _chunk;
	private MeshFilter _meshFilter;
	private MeshCollider _meshCollider;
	private BlockTerrainMeshBuilder _meshBuilder;
	private EventHandler _onModified;
}