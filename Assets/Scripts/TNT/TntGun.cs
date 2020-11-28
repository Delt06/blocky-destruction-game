using UnityEngine;

namespace TNT
{
	public sealed class TntGun : MonoBehaviour
	{
		[SerializeField] private Tnt _tntPrefab = default;
		[SerializeField, Min(0f)] private float _height = 20f;
		[SerializeField, Min(0f)] private float _maxDistance = 100f;
		[SerializeField] private Vector3 _initialVelocity = Vector3.zero;

		private void Update()
		{
			if (!Input.GetMouseButtonDown(0)) return;

			var direction = _camera.ScreenPointToRay(Input.mousePosition);
			if (!Physics.Raycast(direction, out var hit, _maxDistance)) return;

			var tnt = Instantiate(_tntPrefab, hit.point + Vector3.up * _height, Quaternion.identity);
			tnt.GetComponent<Rigidbody>().velocity = _initialVelocity;
		}

		private void Awake()
		{
			_camera = Camera.main;
		}

		private Camera _camera;
	}
}