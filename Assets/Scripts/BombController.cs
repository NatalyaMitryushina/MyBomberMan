using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Common;
using Assets.Scripts.Base;
using System.Collections;

public class BombController : BombControllerBase
	{
		private float explosionDelay = 2f;
		private DynamicObjectsGeneratorBase dynamicObjects;

		void Start()
		{
			dynamicObjects = ObjectCreator.GetDynamicObjects();
		}

		void FixedUpdate()
		{
			DropBomb();			
		}
	
		public override void DropBomb()
		{
			StartCoroutine(Exploder.Explode(this.gameObject, explosionDelay, GetExplostionDistance(), dynamicObjects.GetExplosionSystemPrefab(),
				CheckExplosionDamages));
		}

		private float GetExplostionDistance()
		{
			return 1.5f;
		}

		private void CheckExplosionDamages(RaycastHit[] hittedObjects)
		{
			foreach (RaycastHit hit in hittedObjects)
			{
				GameObject hittedObject = hit.transform.gameObject;
				Debug.Log(hittedObject.tag);
				switch(hittedObject.tag)
				{
					case "Brick Wall":
						//StartCoroutine(DestroyObjectWithFading(hittedObject));
						DestroyObject(hittedObject);
						break;
					case "Enemy":
						DestroyObject(hittedObject);
						break;
					case "Player":
						DestroyObject(hittedObject);
						break;
				}
			}
		}

		private IEnumerator DestroyObjectWithFading(GameObject hittedObject)
		{
			yield return Fade(hittedObject);
			DestroyObject(hittedObject);
		}

		private IEnumerator Fade(GameObject hittedObject)
		{
			Renderer renderer = hittedObject.GetComponent<Renderer>();
			if(renderer != null)
			{
				for(float f = 1f; f > 0; f -= 0.05f)
				{
					Color color = renderer.material.color;
					color.a = f;
					renderer.material.color = color;
					yield return null;

				}
			}
		}
	}

