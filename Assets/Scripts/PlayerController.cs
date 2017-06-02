using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Base;

public class PlayerController : ObjectController
{
	public PlayerController()
	{
		this.speed = 1f;
		this.distance = 1f;
	}
	protected override Vector3 GetDirection()
	{
		return PhysicsHelper.GetDirectionByKey();
	}

	protected override bool CanGo(Vector3 nextPoint)
	{

		nextPoint = transform.position + direction * distance;
		var hitColliders = Physics.OverlapCapsule(nextPoint, nextPoint, 0.5f);
		foreach (var col in hitColliders)
		{
			if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Brick Wall") return false;
		}
		return true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			DestroyObject(this.gameObject);
		}
	}

}