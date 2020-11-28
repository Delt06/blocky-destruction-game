using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Block")]
public sealed class BlockData : ScriptableObject, IBlock
{
	[SerializeField] private SpriteMapping[] _mappings = default;
	[SerializeField] private Sprite _fallbackSprite = default;

	public (Vector2Int textureSize, Rect rect) GetTextureData(WorldSide side)
	{
		var sprite = GetSprite(side);
		var texture = sprite.texture;
		var textureSize = new Vector2Int(texture.width, texture.height);
		return (textureSize, sprite.rect);
	}

	private Sprite GetSprite(WorldSide side)
	{
		foreach (var mapping in _mappings)
		{
			if (mapping.Side == side)
				return mapping.Sprite;
		}

		return _fallbackSprite;
	}

	public bool IsVisible => true;

	[Serializable]
	private struct SpriteMapping
	{
#pragma warning disable 0649

		public WorldSide Side;
		public Sprite Sprite;

#pragma warning restore 0649
	}
}