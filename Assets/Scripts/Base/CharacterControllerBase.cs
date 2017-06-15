using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Base
{
	public abstract class CharacterControllerBase : MonoBehaviour
	{
		protected Vector3 direction;
		protected float speed = PhysicsHelper.CharacterSpeed[SpeedType.Base];
		protected float distance = 1f;
		protected Movement mov;
		protected Vector3 prevPosition;

		protected void Start()
		{
			prevPosition = transform.position;
			mov = new Movement();			
		}

		protected virtual void FixedUpdate()
		{
			prevPosition = transform.position;
			TryMove();
		}

		protected virtual void TryMove()
		{
			direction = GetDirection();
			Vector3 nextPoint = transform.position + direction * distance;
			if (CanGo(nextPoint))
			{
				mov.MoveObject(this, direction, speed, distance);
			}
		}

		public Vector3 PrevPosition
		{
			get { return prevPosition; }
		}

		protected virtual Vector3 GetDirection()
		{
			return PhysicsHelper.GetRandomDirection();
		}

		protected virtual bool CanGo(Vector3 nextPoint)
		{
			var hitColliders = Physics.OverlapCapsule(nextPoint, nextPoint, 0.5f);
			int enemyOverlapCount = 0;
			foreach (var col in hitColliders)
			{
				if (col.gameObject.tag == "Enemy") enemyOverlapCount++;
				if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Brick Wall") return false;
			}
			if (enemyOverlapCount > 1) return false;
			return true;
		}
	}
}
