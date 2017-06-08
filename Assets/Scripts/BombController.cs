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
		private float explostionDistance = 1.5f;
		private DynamicObjectsGeneratorBase dynamicObjects;
		private bool isMoving = false;

		void Start()
		{
			dynamicObjects = ObjectCreator.GetDynamicObjects();
		}

		void Update()
		{
			if(!isMoving)
				DropBomb();	
		}
	
		public override void DropBomb()
		{
			isMoving = true;
			StartCoroutine(Exploder.Explode(this.gameObject, explosionDelay, explostionDistance, dynamicObjects.GetExplosionSystemPrefab(),
				CheckExplosionDamages));	
		}

		private void CheckExplosionDamages(RaycastHit[] hittedObjects)
		{
			foreach (RaycastHit hit in hittedObjects)
			{
				GameObject hittedObject = hit.transform.gameObject;
				switch(hittedObject.tag)
				{
					case "Brick Wall":
						DestroyObject(hittedObject);
						break;
					case "Enemy":
						StartCoroutine(DestroyObjectWithFading(hittedObject));
						break;
					case "Player":
						DestroyObject(hittedObject);
						break;
				}
			}
		}

		private IEnumerator DestroyObjectWithFading(GameObject hittedObject)
		{
			yield return StartCoroutine(hittedObject.Fade());			
			UnityEngine.Object.DestroyObject(hittedObject);		
		}

	}

