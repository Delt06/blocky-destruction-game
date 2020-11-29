using UnityEngine;

namespace Factories
{
	public interface ISpawner<out T>
	{
		T Spawn(Vector3 position, Quaternion rotation);
	}
}