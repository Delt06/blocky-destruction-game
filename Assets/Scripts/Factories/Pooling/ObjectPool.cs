using System.Collections.Generic;
using UnityEngine;

namespace Factories.Pooling
{
	public class ObjectPool : MonoBehaviour, ISpawner<GameObject>
	{
		[SerializeField] private GameObject _prefab = default;
		[SerializeField, Min(0)] private int _capacity = 10;

		public GameObject Spawn(Vector3 position, Quaternion rotation)
		{
			var spawnedObject = _objects.Dequeue();
			spawnedObject.transform.SetPositionAndRotation(position, rotation);

			if (spawnedObject.activeSelf)
				spawnedObject.SetActive(false);

			spawnedObject.SetActive(true);
			_objects.Enqueue(spawnedObject);
			return spawnedObject;
		}

		private void Awake()
		{
			for (var i = 0; i < _capacity; i++)
			{
				var @object = Instantiate(_prefab, transform);
				@object.SetActive(false);
				_objects.Enqueue(@object);
			}
		}

		private readonly Queue<GameObject> _objects = new Queue<GameObject>();
	}
}