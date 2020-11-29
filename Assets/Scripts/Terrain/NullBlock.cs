using UnityEngine;

namespace Terrain
{
	internal sealed class NullBlock : IBlock
	{
		public (Vector2Int textureSize, Rect rect) GetTextureData(WorldSide side) => (Vector2Int.one, Rect.zero);
		public bool IsVisible => false;

		public static IBlock Instance { get; } = new NullBlock();

		private NullBlock() { }
	}
}