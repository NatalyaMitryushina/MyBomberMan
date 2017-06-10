using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Base;
using Assets.Scripts.Common;

namespace Assets.Scripts
{
	class SmartEnemyController : CharacterControllerBase
	{
		public SmartEnemyController()
		{
		}

		protected override Vector3 GetDirection()
		{
			throw new NotImplementedException();
		}

		protected override bool CanGo(Vector3 nextPoint)
		{
			throw new NotImplementedException();
		}

	}
}
