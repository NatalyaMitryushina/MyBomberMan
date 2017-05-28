using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Common
{
	static class Extensions
	{
		public static void Set(this Transform transform, Vector3 nextPosition)
		{
			Vector3 position = transform.position;
			position.x = nextPosition.x;
			position.z = nextPosition.z;
			transform.position = position;
		}
	}
}
