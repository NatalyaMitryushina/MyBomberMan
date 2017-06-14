using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Base;

public class PlayerController : CharacterControllerBase
{
	public PlayerController()
	{
		this.speed = 1f;
	}

	protected override Vector3 GetDirection()
	{
		return PhysicsHelper.GetDirectionByKey();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			DestroyObject(this.gameObject);
		}
	}

}