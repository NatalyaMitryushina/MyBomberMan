using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameField : GameFieldBase
{
	private bool[,] filledPos;
	private StaticObjectsGeneratorBase staticObjects;
    private float wallBottom = 0.5f;
	private int enemyCount = 3;

	private DynamicObjectsGeneratorBase dynamicObjects;

	// Use this for initialization
	void Start () {

        staticObjects = ObjectCreator.StaticObjects();
		filledPos = new bool[_rowCount, _columnCount];
        GenerateField();

		dynamicObjects = ObjectCreator.DynamicObjects();
		GenerateCharacters();

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

					filledPos [i, j] = true;
				}
			}
		}
	}

	private void GenerateBrickWalls()
	{
		GameObject brickWallPrefab = staticObjects.GetBrickWallPrefab();
		System.Random rand = new System.Random();

		for (int k = 0; k < _breakWallsCount; ) {
			int rowPos = rand.Next (1, _rowCount);
			int columPos = rand.Next (1, _columnCount);

			if (!filledPos [rowPos, columPos]) 
			{
				Vector3 position = new Vector3 (columPos, wallBottom, rowPos);
				Instantiate(brickWallPrefab, position, Quaternion.identity);
				filledPos [rowPos, columPos] = true;
				k++;
			}
		}
	}

	private void GenerateCharacters()
	{
		GetPlayer();
		GetEnemies();
		GetPlayer();

		GameObject enemyPrefab = dynamicObjects.GetEnemyPrefab();
		Vector3 pos = new Vector3(0, wallBottom*2, 5);
		Instantiate(enemyPrefab, pos, Quaternion.identity);
	}

	private void GetPlayer()
	{
		GameObject playerPrefab = dynamicObjects.GetPlayerPrefab();
		Vector3 position = new Vector3(0, wallBottom * 2, 0);
		Instantiate(playerPrefab, position, Quaternion.identity);
		FillGameObjectArea(0, 0);
	}

	private void FillGameObjectArea(int indexX, int indexZ)
	{
		filledPos[indexZ, indexX] = true;
	}

	private void GetEnemies()
	{
		System.Random rand = new System.Random();
		int count = enemyCount;
		while(count > 0)
		{
			int rowPos = rand.Next(0, _rowCount);
			int columPos = rand.Next(0, _columnCount);
			if (!filledPos[rowPos, columPos])
			{
				GetEnemy(columPos, rowPos);
				count--;
			}
		}

	}

	private void GetEnemy(int indexX, int indexZ)
	{
		GameObject enemyPrefab = dynamicObjects.GetEnemyPrefab();
		Vector3 pos = new Vector3(indexX, wallBottom * 2, indexZ);
		Instantiate(enemyPrefab, pos, Quaternion.identity);
		FillGameObjectArea(indexX, indexZ);
	}
		
}
