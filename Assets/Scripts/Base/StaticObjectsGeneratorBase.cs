using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticObjectsGeneratorBase {

    public abstract GameObject GetConcreteWallPrefab();
	public abstract GameObject GetBrickWallPrefab(PowerupType powerupType);
	public abstract GameObject GetPowerupPrefab(PowerupType powerupType);
    public abstract GameObject GetFloorPrefab();
}
