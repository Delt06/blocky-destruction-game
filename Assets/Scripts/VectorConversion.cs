using UnityEngine;

public static class VectorConversion
{
	public static Vector3Int Floor(Vector3 vector)
	{
		var x = Mathf.RoundToInt(vector.x);
		var y = Mathf.RoundToInt(vector.y);
		var z = Mathf.RoundToInt(vector.z);
		return new Vector3Int(x, y, z);
	}
}