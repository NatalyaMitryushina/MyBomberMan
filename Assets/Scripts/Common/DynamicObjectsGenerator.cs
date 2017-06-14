using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;
using Assets.Scripts.Common;

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

	public override GameObject GetSmartEnemyPrefab()
	{
		GameObject newGameObject = Resources.Load("Characters/SmartEnemy", typeof(GameObject)) as GameObject;
		if (newGameObject.GetComponent<SmartEnemyController>() == null)
			newGameObject.AddComponent<SmartEnemyController>();
		return newGameObject;
	}

	public override GameObject GetBombPrefab()
	{
		GameObject newGameObject = Resources.Load("Bomb/Bomb", typeof(GameObject)) as GameObject;
		if (newGameObject.GetComponent<BombController>() == null)
			newGameObject.AddComponent<BombController>();

		return newGameObject;
	}
	public override GameObject GetExplosionSystemPrefab()
	{
		GameObject newGameObject = Resources.Load("Bomb/Explosion", typeof(GameObject)) as GameObject;
		return newGameObject;
	}
}
