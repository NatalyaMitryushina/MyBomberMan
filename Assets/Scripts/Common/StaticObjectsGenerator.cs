using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StaticObjectsGenerator : StaticObjectsGeneratorBase {

	public override GameObject GetFloorPrefab()
	{
		GameObject newGameObject = Resources.Load("Floor", typeof(GameObject)) as GameObject;
		return newGameObject;
	}
	public override GameObject GetConcreteWallPrefab()
	{
        GameObject newGameObject = Resources.Load("Walls/ConcreteWall", typeof(GameObject)) as GameObject;
		newGameObject.transform.localScale = new Vector3 (0.9f, 1, 0.9f);
        return newGameObject; 
    }
	public override GameObject GetBrickWallPrefab(PowerupType powerupType)
	{
		GameObject newGameObject = Resources.Load("Walls/BrickWall", typeof(GameObject)) as GameObject;
		if (newGameObject.GetComponent<BrickWallController>() == null)
			newGameObject.AddComponent<BrickWallController>();
		newGameObject.GetComponent<BrickWallController>().SetPowerupType(powerupType);
		return newGameObject;
	}
	public override GameObject GetPowerupPrefab(PowerupType powerupType)
	{
		string newGameObjectPath = "";
		switch(powerupType)
		{
			case PowerupType.Bombs:
				newGameObjectPath = "Powerup/BombBoll";
				break;
			case PowerupType.Flames:
				newGameObjectPath = "Powerup/FlamesBoll";
				break;
			case PowerupType.Speed:
				newGameObjectPath = "Powerup/SpeedBoll";
				break;
			case PowerupType.Wallpass:
				newGameObjectPath = "Powerup/WallpassBoll";
				break;
		}
		GameObject newGameObject = Resources.Load(newGameObjectPath, typeof(GameObject)) as GameObject;
		if (newGameObject.GetComponent<Powerup>() == null)
			newGameObject.AddComponent<Powerup>();
		return newGameObject;
	}

}
