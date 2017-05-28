using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Common;

	class PhysicsHelper
	{
		public static System.Random rand = new System.Random();

		public static Vector3 GetRandomDirection()
		{
			var possibleDirections = new List<Vector3>
			{ 
				Vector3.forward, 
				Vector3.back, 
				Vector3.right, 
				Vector3.left
			};

			return possibleDirections[rand.Next(0, possibleDirections.Count)];
		}

		public static Vector3 GetDirectionByKey()
		{
			if (Input.GetKey(KeyCode.UpArrow)) return Vector3.forward; 
			if (Input.GetKey(KeyCode.DownArrow)) return Vector3.back;
			if (Input.GetKey(KeyCode.RightArrow)) return Vector3.right;
			if (Input.GetKey(KeyCode.LeftArrow)) return Vector3.left;
			return Vector3.zero;
		}

		public static void RotateObject(Transform transform, Vector3 direction)
		{
			float rotationAngle = 0f;
			if (direction.x != 0)
			{
				rotationAngle = 90 * direction.x;
			}
			else if (direction.z < 0)
			{
				rotationAngle = 180;
			}
			transform.eulerAngles = new Vector3(0, rotationAngle, 0);
		}
	}
