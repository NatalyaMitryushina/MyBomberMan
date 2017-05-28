using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Common
{
	class Movement 
	{
		private bool isMoving = false;
		private Vector3 direction;
		private float speed;
		private float distance;
		private float movementDuration = 0.05f;
		private float smoothing = 7f;

		public void TryMove(MonoBehaviour gameObjectBehavior,  Vector3 direct, float sp, float dist)
		{
			direction = direct;
			speed = sp;
			distance = dist;

			if (!isMoving && direction != Vector3.zero) Move(gameObjectBehavior);
		}

		private void Move(MonoBehaviour gameObjectBehavior)
		{
			PhysicsHelper.RotateObject(gameObjectBehavior.transform, direction);
			Vector3 start = gameObjectBehavior.transform.position;
			Vector3 end = gameObjectBehavior.transform.position + direction * distance;
			IEnumerator makeSteps = MakeSteps(gameObjectBehavior.transform, start, end);

			gameObjectBehavior.StartCoroutine(makeSteps);
		}

		private IEnumerator MakeSteps(Transform transform, Vector3 start, Vector3 end)
		{
			isMoving = true;
			float t = Time.deltaTime * smoothing * speed;
			while (Vector3.Distance(transform.position, end) > movementDuration)
			{
				transform.Set(Vector3.Lerp(transform.position, end, t));
				yield return new WaitForEndOfFrame();
			}
			transform.Set(end);
			isMoving = false;
		}
	}
}
