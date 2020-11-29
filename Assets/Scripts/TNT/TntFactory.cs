using Factories;
using UnityEngine;

namespace TNT
{
	public sealed class TntFactory : FactoryBase<Tnt>, ITntFactory
	{
		public Tnt Create(Vector3 position, Quaternion rotation) => Spawn(position, rotation);
	}
}