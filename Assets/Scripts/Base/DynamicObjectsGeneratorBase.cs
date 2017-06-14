using Assets.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicObjectsGeneratorBase
{
	public abstract GameObject GetPlayerPrefab();
	public abstract GameObject GetEnemyPrefab();
	public abstract GameObject GetSmartEnemyPrefab();
	public abstract GameObject GetBombPrefab();
	public abstract GameObject GetExplosionSystemPrefab();
}
