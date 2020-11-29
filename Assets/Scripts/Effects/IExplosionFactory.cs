using UnityEngine;

namespace Effects
{
	public interface IExplosionFactory
	{
		ParticleSystem Create(Vector3 position, Quaternion rotation);
	}
}