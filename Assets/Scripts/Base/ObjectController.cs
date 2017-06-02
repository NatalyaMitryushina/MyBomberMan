using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Base
{
	public abstract class ObjectController : MonoBehaviour
	{
		protected Vector3 direction;
		protected float speed;
		protected float distance;
		protected Movement mov;

		void Start()
		{
			mov = new Movement();
		}

		void Update()
		{
			direction = GetDirection();
			Vector3 nextPoint = transform.position + direction * distance;
			if (CanGo(nextPoint)) mov.TryMove(this, direction, speed, distance);
		}

		protected abstract Vector3 GetDirection();
		protected abstract bool CanGo(Vector3 nextPoint);
	}
}
