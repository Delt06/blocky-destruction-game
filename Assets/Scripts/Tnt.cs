using UnityEngine;

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
		
		Destroy(gameObject);
	}

	private readonly Collider[] _contacts = new Collider[16];
}