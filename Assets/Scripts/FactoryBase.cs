using System;
using UnityEngine;

public abstract class FactoryBase<T> : MonoBehaviour
{
	protected T Spawn(Vector3 position, Quaternion rotation)
	{
		var spawnedObject = _spawner.Spawn(position, rotation);
		if (!spawnedObject.TryGetComponent(out T component))
			throw new InvalidOperationException($"Spawned object does not have a {nameof(T)}.");

		return component;
	}

	public void Construct(ISpawner<GameObject> spawner)
	{
		_spawner = spawner;
	}

	private ISpawner<GameObject> _spawner;
}