using UnityEngine;

namespace TNT
{
	public interface ITntFactory
	{
		Tnt Create(Vector3 position, Quaternion rotation);
	}
}