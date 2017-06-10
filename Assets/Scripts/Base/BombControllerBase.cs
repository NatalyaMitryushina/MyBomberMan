using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Base
{
	public abstract class BombControllerBase : MonoBehaviour
	{
		protected float explosionDelay = 0f;
		protected float explosionDistance = 1f;
		protected float explosionDuration = 1f;

		public float ExplosionDelay { 
			get { return explosionDelay; } 
		}
		public float ExplostionDistance
		{
			get { return explosionDistance; }
		}
		public float ExplostionDuration
		{
			get { return explosionDuration; }
		}

		public abstract void DropBomb();
	}
}
