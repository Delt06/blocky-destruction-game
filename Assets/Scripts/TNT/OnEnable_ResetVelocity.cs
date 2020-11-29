using UnityEngine;

namespace TNT
{
	public sealed class OnEnable_ResetVelocity : MonoBehaviour
	{
		private void OnEnable()
		{
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
		}

		public void Construct(Rigidbody rigidbody) => _rigidbody = rigidbody;

		private Rigidbody _rigidbody;
	}
}