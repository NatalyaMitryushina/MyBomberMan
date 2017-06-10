using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Base;
using Assets.Scripts.Common;

public class GameField : GameFieldBase
{
	public StaticObjectsGeneratorBase staticObjects;
	public DynamicObjectsGeneratorBase dynamicObjects;

	private GameFieldPositionsManager gameFieldPositionsManager;
    private float wallBottom = 0.5f;
	private int enemyCount = 3;
	private int smartEnemyCount = 1;
	GameObject player;

	void Start ()
	{
        staticObjects = ObjectCreator.GetStaticObjects();

		gameFieldPositionsManager = new GameFieldPositionsManager(_columnCount, _rowCount);
        GenerateField();

		dynamicObjects = ObjectCreator.GetDynamicObjects();
		GenerateCharacters();

	}

	void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameObject bombPrefab = dynamicObjects.GetBombPrefab();
			Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y - 0.9f, player.transform.position.z);
			GameObject bombObject = Instantiate(bombPrefab, position, Quaternion.identity);

			float bombActionTime = bombObject.GetComponent<BombControllerBase>().ExplosionDelay 
				+ bombObject.GetComponent<BombControllerBase>().ExplostionDuration;
			StartCoroutine(FreeBombArea(bombObject, bombActionTime));
		}
	}

	private IEnumerator FreeBombArea(GameObject bombObject, float bombActionTime)
	{
		yield return new WaitForSeconds(bombActionTime);

		int xBombPos = (int)bombObject.transform.position.x;
		int zBombPos = (int)bombObject.transform.position.z;
		gameFieldPositionsManager.FreeGameFieldPositionArea(xBombPos, zBombPos, bombObject.GetComponent<BombControllerBase>().ExplostionDistance);

		UnityEngine.Object.DestroyObject(bombObject);
	}

    public override void GenerateField()
    {
		GameObject floorPrefab = staticObjects.GetFloorPrefab();
        Instantiate(floorPrefab, new Vector3(_columnCount / 2, 0, _rowCount / 2), Quaternion.identity);

        GenerateBorder();
		GenerateConcreteWalls();
		GenerateBrickWalls();
    }

    private void GenerateBorder()
    {
        GameObject concreteWallPrefab = staticObjects.GetConcreteWallPrefab();

        for (int j = -1; j <= _columnCount; j++)
        {
			Vector3 position = new Vector3(j, wallBottom, -1);
            Instantiate(concreteWallPrefab, position, Quaternion.identity);

			position = new Vector3(j, wallBottom, _rowCount);
            Instantiate(concreteWallPrefab, position, Quaternion.identity);
        }

        for (int i = -1; i < _rowCount; i++)
        {
			Vector3 position = new Vector3(-1, wallBottom, i);
            Instantiate(concreteWallPrefab, position, Quaternion.identity);

			position = new Vector3(_columnCount, wallBottom, i);
            Instantiate(concreteWallPrefab, position, Quaternion.identity);
        }
    }

	private void GenerateConcreteWalls()
	{
		GameObject concreteWallPrefab = staticObjects.GetConcreteWallPrefab();
		for (int i = 0; i < _rowCount; i++) 
		{
			for (int j = 0; j < _columnCount; j++) 
			{
				if (i % 2 != 0 && j % 2 != 0) 
				{
					Vector3 position = new Vector3(j, wallBottom, i);
					Instantiate(concreteWallPrefab, position, Quaternion.identity);

					gameFieldPositionsManager.FillGameFieldPosition(j, i, concreteWallPrefab.tag);
				}
			}
		}
	}

	private void GenerateBrickWalls()
	{
		GameObject brickWallPrefab = staticObjects.GetBrickWallPrefab();
		System.Random rand = new System.Random();

		for (int k = 0; k < _breakWallsCount; ) {
			int zPos = rand.Next (1, _rowCount);
			int xPos = rand.Next (1, _columnCount);

			if (!gameFieldPositionsManager.IsFilled(xPos, zPos)) 
			{
				Vector3 position = new Vector3 (xPos, wallBottom, zPos);
				Instantiate(brickWallPrefab, position, Quaternion.identity);
				gameFieldPositionsManager.FillGameFieldPosition(xPos, zPos, brickWallPrefab.tag);
				k++;
			}
		}
	}

	private void GenerateCharacters()
	{
		GeneratePlayer();
		GenerateEnemies();
	}

	private void GeneratePlayer()
	{
		GameObject playerPrefab = dynamicObjects.GetPlayerPrefab();
		Vector3 position = new Vector3(0, wallBottom * 2, 0);
		player = Instantiate(playerPrefab, position, Quaternion.identity);
		gameFieldPositionsManager.FillGameFieldPosition(0, 0, player.tag);
	}

	private void GenerateEnemies()
	{
		System.Random rand = new System.Random();
		int count = enemyCount + smartEnemyCount;
		while(count > 0)
		{
			int zPos = rand.Next(0, _rowCount);
			int xPos = rand.Next(0, _columnCount);
			if (!gameFieldPositionsManager.IsFilled(xPos, zPos))
			{
				if (count > enemyCount)
				{
					GenerateEnemy(xPos, zPos, dynamicObjects.GetSmartEnemyPrefab());
				}
				else
				{
					GenerateEnemy(xPos, zPos, dynamicObjects.GetEnemyPrefab());
				}
				count--;
			}
		}
	}

	private void GenerateEnemy(int indexX, int indexZ, GameObject enemyPrefab)
	{
		Vector3 pos = new Vector3(indexX, wallBottom * 2, indexZ);
		Instantiate(enemyPrefab, pos, Quaternion.identity);
		gameFieldPositionsManager.FillGameFieldPosition(indexX, indexZ, enemyPrefab.tag);
	}
		
}
