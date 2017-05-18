using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticObjectsGeneratorBase {

    public abstract GameObject GetConcreteWallPrefab();
    public abstract GameObject GetBrickWallPrefab();
    public abstract GameObject GetFloorPrefab();
}
