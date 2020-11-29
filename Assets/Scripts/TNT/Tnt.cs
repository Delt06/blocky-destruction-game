using System;
using Terrain;
using UnityEngine;

namespace TNT
{
	public sealed class Tnt : MonoBehaviour
	{
		[SerializeField] private float _radius = 5f;
		[SerializeField] private ExplosionMode _onExploded = ExplosionMode.Destroy;

		private void OnCollisionEnter(Collision other)
		{
			Explode();
		}

		public void Explode()
		{
			var contactsCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _contacts);

			for (var i = 0; i < contactsCount; i++)
			{
				var contact = _contacts[i];
				if (!contact.TryGetComponent(out Chunk chunk)) continue;

				chunk.SetSphere(transform.position, _radius, NullBlock.Instance);
			}

			Exploded?.Invoke(this, EventArgs.Empty);

			switch (_onExploded)
			{
				case ExplosionMode.Destroy:
					Destroy(gameObject);
					break;
				case ExplosionMode.Disable:
					gameObject.SetActive(false);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public event EventHandler Exploded;

		private readonly Collider[] _contacts = new Collider[16];

		private enum ExplosionMode
		{
			Destroy,
			Disable
		}
	}
}