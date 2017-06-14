using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectCreator
{
	private static StaticObjectsGeneratorBase staticObjects;
	private static DynamicObjectsGeneratorBase dynamicObjects;
	private static GameFieldPositionsManager gameFieldPositionsManager;

	public static StaticObjectsGeneratorBase GetStaticObjects()
	{
		if (staticObjects == null) staticObjects = new StaticObjectsGenerator();
		return staticObjects;
	}

	public static DynamicObjectsGeneratorBase GetDynamicObjects()
	{
		if (dynamicObjects == null) dynamicObjects = new DynamicObjectsGenerator();
		return dynamicObjects;
	}

	public static void SetGameFieldPositionsManager(this GameFieldBase gameFieldBase, GameFieldPositionsManager gameFieldPosMan)
	{
		gameFieldPositionsManager = gameFieldPosMan;
	}

	public static GameFieldPositionsManager GetGameFieldPositionsManager()
	{
		if (gameFieldPositionsManager == null) throw new InvalidOperationException("Error! Game Field has not been created yet!");
		return gameFieldPositionsManager;
	}
		
}
