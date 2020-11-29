using UnityEngine;

namespace TNT
{
	public sealed class TntDelayedExplosion : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _delay = 1f;

		private void Update()
		{
			_elapsedTime += Time.deltaTime;
			if (_elapsedTime < _delay) return;

			_tnt.Explode();
		}

		private void OnEnable()
		{
			_elapsedTime = 0f;
		}

		public void Construct(Tnt tnt) => _tnt = tnt;

		private float _elapsedTime;
		private Tnt _tnt;
	}
}