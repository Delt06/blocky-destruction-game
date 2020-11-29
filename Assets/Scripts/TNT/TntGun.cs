using UnityEngine;

namespace TNT
{
	public sealed class TntGun : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _height = 20f;
		[SerializeField, Min(0f)] private float _maxDistance = 100f;
		[SerializeField] private Vector3 _initialVelocity = Vector3.zero;

		private void Update()
		{
			if (!Input.GetMouseButtonDown(0)) return;

			var direction = _camera.ScreenPointToRay(Input.mousePosition);
			if (!Physics.Raycast(direction, out var hit, _maxDistance)) return;

			var position = hit.point + Vector3.up * _height;
			var tnt = _tntFactory.Create(position, Quaternion.identity);
			tnt.GetComponent<Rigidbody>().velocity = _initialVelocity;
		}

		public void Construct(Camera camera, ITntFactory tntFactory)
		{
			_camera = camera;
			_tntFactory = tntFactory;
		}

		private Camera _camera;
		private ITntFactory _tntFactory;
	}
}