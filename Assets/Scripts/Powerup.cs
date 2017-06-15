using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum PowerupType
{
	Bombs, 
	Flames,
	Speed,
	Wallpass,
	_PowerupTypesCount,
	_None
}

public class Powerup : MonoBehaviour
{
	public PowerupType powerupType;
	private float speedRotationRatio = 1.5f;

	void Update()
	{
		transform.Rotate(new Vector3(30, 30, 30) * speedRotationRatio * Time.deltaTime);
	}
}
 