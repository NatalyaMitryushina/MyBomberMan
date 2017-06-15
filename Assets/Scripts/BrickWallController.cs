using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
	class BrickWallController : MonoBehaviour
	{
		public GameObject powerup;
		public PowerupType powerupType;
		public void SetPowerupType(PowerupType powerupType)
		{
			this.powerupType = powerupType;
		}

		void Start()
		{
			if (powerupType != PowerupType._None)
			{
				powerup = ObjectCreator.GetStaticObjects().GetPowerupPrefab(powerupType);
				Instantiate(powerup, transform.position, Quaternion.identity);
			}
		}
	}
}
