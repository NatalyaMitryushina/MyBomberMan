using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaticObjectsGenerator : StaticObjectsGeneratorBase {

	public override GameObject GetConcreteWallPrefab()
	{
        GameObject newGameObject = Resources.Load("Walls/ConcreteWall", typeof(GameObject)) as GameObject;
		newGameObject.transform.localScale = new Vector3 (0.9f, 1, 0.9f);
        return newGameObject; 
    }
    public override GameObject GetBrickWallPrefab()
    {
        GameObject newGameObject = Resources.Load("Walls/BrickWall", typeof(GameObject)) as GameObject;
        return newGameObject;
    }
    public override GameObject GetFloorPrefab()
    {
        GameObject newGameObject = Resources.Load("Floor", typeof(GameObject)) as GameObject;
        return newGameObject;
    }
}
