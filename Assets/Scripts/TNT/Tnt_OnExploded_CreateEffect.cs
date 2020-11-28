using UnityEngine;

namespace TNT
{
	public sealed class Tnt_OnExploded_CreateEffect : Tnt_OnExploded_Base
	{
		[SerializeField] private GameObject _effectPrefab = default;

		protected override void OnExploded()
		{
			Instantiate(_effectPrefab, transform.position, Quaternion.identity);
		}
	}
}