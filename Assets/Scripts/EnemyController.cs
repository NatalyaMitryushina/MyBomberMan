using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Common;
using Assets.Scripts.Base;

public class EnemyController : CharacterControllerBase
{
	public EnemyController()
	{
		this.speed = PhysicsHelper.CharacterSpeed[SpeedType.Slow];
	}

}

