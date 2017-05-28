using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DynamicObjectsGenerator : DynamicObjectsGeneratorBase
{
	public override GameObject GetPlayerPrefab()
	{
		GameObject newGameObject = Resources.Load("Characters/Player", typeof(GameObject)) as GameObject;
		if (newGameObject.GetComponent<PlayerController>() == null)
			newGameObject.AddComponent<PlayerController>();
		return newGameObject;
	}

	public override GameObject GetEnemyPrefab()
	{
		GameObject newGameObject = Resources.Load("Characters/Enemy", typeof(GameObject)) as GameObject;
		if (newGameObject.GetComponent<EnemyController>() == null)
			newGameObject.AddComponent<EnemyController>();
		return newGameObject;
	}
}
