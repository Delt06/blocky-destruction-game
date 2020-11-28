using System;
using UnityEngine;

namespace TNT
{
	public abstract class Tnt_OnExploded_Base : MonoBehaviour
	{
		public void Construct(Tnt tnt)
		{
			Tnt = tnt;
		}

		protected virtual void OnEnable()
		{
			Tnt.Exploded += _onExploded;
		}

		protected virtual void OnDisable()
		{
			Tnt.Exploded -= _onExploded;
		}

		protected virtual void Awake()
		{
			Tnt = GetComponentInParent<Tnt>();
			_onExploded = (sender, args) => OnExploded();
		}

		protected abstract void OnExploded();

		protected Tnt Tnt { get; private set; }
		private EventHandler _onExploded;
	}
}