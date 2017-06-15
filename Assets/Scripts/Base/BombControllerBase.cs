using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Base
{
	public abstract class BombControllerBase : MonoBehaviour
	{
		protected float explosionDelay = 2f;
		protected float baseExplosionDistance = 1f;
		protected float additionalExplosionDistance;
		protected float explosionDuration = 1f;

		public float ExplosionDelay { 
			get { return explosionDelay; } 
		}
		public float ExplosionDistance
		{
			get { return baseExplosionDistance + additionalExplosionDistance; }
		}
		public float ExplosionDuration
		{
			get { return explosionDuration; }
		}
		public float ActionTime
		{
			get
			{
				return this.explosionDelay + this.explosionDuration;
			}
		}
		public void AddAdditionalExplosionDistance(float addDistance)
		{
			additionalExplosionDistance = addDistance;
		}
		public void ResetAdditionalDistance()
		{
			additionalExplosionDistance = 0f;
		}
		
		public abstract void DropBomb();
	}
}
