using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Common;
using Assets.Scripts.Base;

public class EnemyController : ObjectController
{
	public EnemyController()
	{
		this.speed = 0.3f;
		this.distance = 1f;
	}

	protected override Vector3 GetDirection()
	{
		return PhysicsHelper.GetRandomDirection();
	}

	protected override bool CanGo(Vector3 nextPoint)
	{
		var hitColliders = Physics.OverlapCapsule(nextPoint, nextPoint, 0.5f);
		int enemyOverlapCount = 0;
		foreach (var col in hitColliders)
		{
			if (col.gameObject.tag == "Enemy") enemyOverlapCount++;
			if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Brick Wall") return false;
		}
		if (enemyOverlapCount > 1) return false;
		return true;
	}

}

