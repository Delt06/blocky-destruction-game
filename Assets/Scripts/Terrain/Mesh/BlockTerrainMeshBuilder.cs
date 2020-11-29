using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Terrain.Mesh
{
	public sealed class BlockTerrainMeshBuilder
	{
		public BlockTerrainMeshBuilder AtPosition(Vector3 position)
		{
			_position = position;
			return this;
		}

		public BlockTerrainMeshBuilder WithBlock([NotNull] IBlock block)
		{
			_block = block ?? throw new ArgumentNullException(nameof(block));
			return this;
		}

		public BlockTerrainMeshBuilder AddSide(WorldSide side)
		{
			var vertices = WorldSideVertices[side];
			Quad(side, vertices[0], vertices[1], vertices[2], vertices[3]);
			return this;
		}

		private void Quad(WorldSide side, Vector3 leftBottom, Vector3 leftTop, Vector3 rightTop, Vector3 rightBottom)
		{
			var initialVerticesCount = _vertices.Count;

			Vertex(side, leftBottom, Corner.LeftBottom);
			Vertex(side, leftTop, Corner.LeftTop);
			Vertex(side, rightTop, Corner.RightTop);
			Vertex(side, rightBottom, Corner.RightBottom);

			Triangle(initialVerticesCount, initialVerticesCount + 1, initialVerticesCount + 2);
			Triangle(initialVerticesCount, initialVerticesCount + 2, initialVerticesCount + 3);
		}

		private void Vertex(WorldSide side, Vector3 position, Corner corner)
		{
			_vertices.Add(_position + position);
			_uvs.Add(GetBlockCornerUv(side, corner));
		}

		private Vector2 GetBlockCornerUv(WorldSide side, Corner corner)
		{
			GetBlockCornerData(side, corner, out var textureSize, out var position);
			var uv = position / textureSize;

			return uv;
		}

		private void GetBlockCornerData(WorldSide side, Corner corner, out Vector2Int textureSize, out Vector2 position)
		{
			Rect rect;
			(textureSize, rect) = _block.GetTextureData(side);

			switch (corner)
			{
				case Corner.LeftBottom:
					position = rect.min + Vector2.one * _bordersInPixels;
					break;
				case Corner.LeftTop:
					position = new Vector2(rect.xMin, rect.yMax);
					position.x += _bordersInPixels;
					position.y -= _bordersInPixels;
					break;
				case Corner.RightTop:
					position = rect.max - Vector2.one * _bordersInPixels;
					break;
				case Corner.RightBottom:
					position = new Vector2(rect.xMax, rect.yMin);
					position.x -= _bordersInPixels;
					position.y += _bordersInPixels;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(corner), corner, null);
			}
		}

		private void Triangle(int vertex0, int vertex1, int vertex2)
		{
			_triangles.Add(vertex0);
			_triangles.Add(vertex1);
			_triangles.Add(vertex2);
		}

		public UnityEngine.Mesh Build()
		{
			_mesh.SetVertices(_vertices);
			_mesh.SetTriangles(_triangles, 0);
			_mesh.SetUVs(0, _uvs);
			_mesh.RecalculateBounds();
			_mesh.RecalculateNormals();
			_mesh.RecalculateTangents();
			return _mesh;
		}

		public BlockTerrainMeshBuilder Clear()
		{
			_vertices.Clear();
			_triangles.Clear();
			_uvs.Clear();
			_mesh.Clear();
			return this;
		}

		public BlockTerrainMeshBuilder(UnityEngine.Mesh mesh, float bordersInPixels)
		{
			_mesh = mesh;
			_bordersInPixels = bordersInPixels;
		}

		private Vector3 _position;
		private readonly float _bordersInPixels;
		private IBlock _block = NullBlock.Instance;

		private readonly UnityEngine.Mesh _mesh;
		private readonly List<Vector3> _vertices = new List<Vector3>();
		private readonly List<int> _triangles = new List<int>();
		private readonly List<Vector2> _uvs = new List<Vector2>();

		private static readonly Dictionary<WorldSide, Vector3[]> WorldSideVertices =
			new Dictionary<WorldSide, Vector3[]>
			{
				[WorldSide.North] = new[] {new Vector3(1, 0, 1), Vector3.one, new Vector3(0, 1, 1), Vector3.forward},
				[WorldSide.South] = new[] {Vector3.zero, Vector3.up, new Vector3(1, 1), Vector3.right},
				[WorldSide.East] = new[] {Vector3.right, new Vector3(1, 1), Vector3.one, new Vector3(1, 0, 1)},
				[WorldSide.West] = new[] {Vector3.forward, new Vector3(0, 1, 1), Vector3.up, Vector3.zero},
				[WorldSide.Top] = new[] {Vector3.up, new Vector3(0, 1, 1), Vector3.one, new Vector3(1, 1, 0)},
				[WorldSide.Bottom] = new[] {Vector3.forward, Vector3.zero, Vector3.right, new Vector3(1, 0, 1)}
			};
	}
}