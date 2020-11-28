using System;
using UnityEngine;

namespace TNT
{
	public sealed class Tnt : MonoBehaviour
	{
		[SerializeField] private float _radius = 5f;

		private void OnCollisionEnter(Collision other)
		{
			Explode();
		}

		private void Explode()
		{
			var contactsCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _contacts);

			for (var i = 0; i < contactsCount; i++)
			{
				var contact = _contacts[i];
				if (!contact.TryGetComponent(out Chunk chunk)) continue;

				chunk.SetSphere(transform.position, _radius, NullBlock.Instance);
			}

			Exploded?.Invoke(this, EventArgs.Empty);
			Destroy(gameObject);
		}

		public event EventHandler Exploded;

		private readonly Collider[] _contacts = new Collider[16];
	}
}