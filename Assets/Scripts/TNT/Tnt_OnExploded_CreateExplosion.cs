using Effects;
using UnityEngine;

namespace TNT
{
	public sealed class Tnt_OnExploded_CreateExplosion : Tnt_OnExploded_Base
	{
		protected override void OnExploded()
		{
			_explosionFactory.Create(transform.position, Quaternion.identity);
		}

		public void Construct(IExplosionFactory explosionFactory)
		{
			_explosionFactory = explosionFactory;
		}

		private IExplosionFactory _explosionFactory;
	}
}