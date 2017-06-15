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
	private int enemyCount = 2;
	private int smartEnemyCount = 2;
	GameObject player;

	GameField()
	{
		gameFieldPositionsManager = new GameFieldPositionsManager(_columnCount, _rowCount);
		this.SetGameFieldPositionsManager(gameFieldPositionsManager);	
	}

	void Start ()
	{
        staticObjects = ObjectCreator.GetStaticObjects();
		dynamicObjects = ObjectCreator.GetDynamicObjects();

        GenerateField();
		GenerateCharacters();

		gameFieldPositionsManager.ClearCharactersFieldPositions();
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
		GameObject brickWallPrefab;
		System.Random rand = new System.Random();

		for (int k = 0; k < _breakWallsCount;) {
			int zPos = rand.Next (1, _rowCount);
			int xPos = rand.Next (1, _columnCount);

			if (!gameFieldPositionsManager.IsFilled(xPos, zPos)) 
			{
				Vector3 position = new Vector3 (xPos, wallBottom, zPos);
				if(k < (int) PowerupType._PowerupTypesCount)
				{
					brickWallPrefab = staticObjects.GetBrickWallPrefab((PowerupType) k);
				}
				else
				{
					brickWallPrefab = staticObjects.GetBrickWallPrefab(PowerupType._None);
				}
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
		player.GetComponent<PlayerController>().Bomb = dynamicObjects.GetBombPrefab();

		int xPos = Convert.ToInt32(Math.Round(player.transform.position.x));
		int zPos = Convert.ToInt32(Math.Round(player.transform.position.z));
		gameFieldPositionsManager.FillGameFieldPosition(xPos, zPos, player.tag);
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
					GameObject smartEnemy = dynamicObjects.GetSmartEnemyPrefab();
					GenerateEnemy(xPos, zPos, smartEnemy);
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
