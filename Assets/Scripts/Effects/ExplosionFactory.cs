using UnityEngine;

namespace Effects
{
	public sealed class ExplosionFactory : FactoryBase<ParticleSystem>, IExplosionFactory
	{
		public ParticleSystem Create(Vector3 position, Quaternion rotation)
		{
			var particles = Spawn(position, rotation);
			particles.Clear();
			particles.Play();
			return particles;
		}
	}
}