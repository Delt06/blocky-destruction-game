using UnityEngine;

public interface IBlock
{
	(Vector2Int textureSize, Rect rect) GetTextureData(WorldSide side);
	bool IsVisible { get; }
}