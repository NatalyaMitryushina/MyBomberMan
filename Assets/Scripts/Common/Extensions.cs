using System;
using System.Collections;
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

		public static IEnumerator Fade(this GameObject hittedObject)
		{
			Renderer[] renderers = hittedObject.GetComponentsInChildren<Renderer>();
			if (renderers != null)
			{
				for (float f = 1f; f >= 0f; f -= 0.05f)
				{
					foreach (var renderer in renderers)
					{
						Color color = renderer.material.color;
						color.a = f;
						renderer.material.color = color;
					}
					yield return null;
				}
			}
		}

		public static void Invisible(this GameObject hittedObject)
		{
			Renderer renderer = hittedObject.GetComponent<Renderer>();
			if (renderer != null)
			{
				Color color = renderer.material.color;
				color.a = 0f;
				renderer.material.color = color;
			}

		}
	}
}
