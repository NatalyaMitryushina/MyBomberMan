using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectCreator
{
	private static StaticObjectsGeneratorBase staticObjects;
	private static DynamicObjectsGeneratorBase dynamicObjects;

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
		
}
