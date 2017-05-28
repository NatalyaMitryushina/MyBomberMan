using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Common;

public class EnemyController : MonoBehaviour
{
	private Vector3 direction;
	private float speed = 0.5f;
	private float distance = 1f; 
	private Movement mov;

	void Start()
	{
		mov = new Movement();
	}

	void Update()
	{
		direction = PhysicsHelper.GetRandomDirection();
		Vector3 nextPoint = transform.position + direction * distance;
		if (!IsPossible(nextPoint)) mov.TryMove(this, direction, speed, distance);
	}

	private bool IsPossible(Vector3 nextPoint)
	{
		var hitColliders = Physics.OverlapCapsule(nextPoint, nextPoint, 0.5f);
		int enemyOverlapCount = 0;
		foreach (var col in hitColliders)
		{
			if (col.gameObject.tag == "Enemy") enemyOverlapCount++;
			if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Brick Wall") return true;
		}
		if (enemyOverlapCount > 1) return true;
		return false;
	}

}

