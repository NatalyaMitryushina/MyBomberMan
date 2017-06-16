using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Base;
using System.Collections;

namespace Assets.Scripts.Common
{
	public class GameFieldPositionsManager
	{
		public List<Point> Directions = new List<Point>()
		{
			new Point(1, 0),
			new Point(-1, 0),
			new Point(0, 1),
			new Point(0, -1)
		};

		private struct GameFieldPosition
		{
			public bool isFilled;
			public string objectTag;
		};

		private GameFieldPosition[,] gameFieldPositions;
		private int xCount;
		private int zCount;

		public GameFieldPositionsManager(int xCount, int zCount)
		{
			gameFieldPositions = new GameFieldPosition[xCount, zCount];
			this.xCount = xCount;
			this.zCount = zCount;
		}

		public void FreeGameFieldPositionArea(int x, int z, float radius)
		{
			int fieldPosCount;
			fieldPosCount = Convert.ToInt32(Math.Round(radius));

			for (var i = 0; i < fieldPosCount; i++)
			{
				foreach (var dir in Directions)
				{
					if (InBounds(x + dir.x + i, z + dir.z + i)) FreeGameFieldPosition(x + dir.x + i, z + dir.z + i);
				}
			}
			FreeGameFieldPosition(x, z);
		}

		public bool InBounds(int x, int z)
		{
			return x >= 0 && x < xCount
				&& z >= 0 && z < zCount;
		}

		public void FillGameFieldPosition(int x, int z, string objectTag)
		{
			gameFieldPositions[x, z].isFilled = true;
			gameFieldPositions[x, z].objectTag = objectTag;
		}

		public void FreeGameFieldPosition(int x, int z)
		{
			gameFieldPositions[x, z].isFilled = false;
			gameFieldPositions[x, z].objectTag = "";
		}

		public bool IsFilled(int x, int z)
		{
			if (!InBounds(x, z)) return false;
			if (gameFieldPositions[x, z].isFilled) return true;
			return false;
		}

		public void UpdateCharactersFieldPositions()
		{
			var player = GameObject.FindGameObjectWithTag("Player");
			if (player != null) ChangeCharacterPosition(player);

			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (var enemy in enemies)
			{
				if (enemy != null) ChangeCharacterPosition(enemy);
			}
		}

		private void ChangeCharacterPosition(GameObject character)
		{
			Vector3 prevPos = character.GetComponent<CharacterControllerBase>().PrevPosition;
			if (prevPos != character.transform.position)
			{
				int prevX = Convert.ToInt32(Math.Round(prevPos.x));
				int prevZ = Convert.ToInt32(Math.Round(prevPos.z));
				FreeGameFieldPosition(prevX, prevZ);
				int nextX = Convert.ToInt32(Math.Round(character.transform.position.x));
				int nextZ = Convert.ToInt32(Math.Round(character.transform.position.z));
				FillGameFieldPosition(nextX, nextZ, character.tag);
			}
		}

		public void ClearCharactersFieldPositions()
		{
			var player = GameObject.FindGameObjectWithTag("Player");
			if (player != null)
			{
				int prevX = Convert.ToInt32(Math.Round(player.transform.position.x));
				int prevZ = Convert.ToInt32(Math.Round(player.transform.position.z));
				FreeGameFieldPosition(prevX, prevZ);
			}

			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (var enemy in enemies)
			{
				if (enemy != null)
				{
					int prevX = Convert.ToInt32(Math.Round(enemy.transform.position.x));
					int prevZ = Convert.ToInt32(Math.Round(enemy.transform.position.z));
					FreeGameFieldPosition(prevX, prevZ);
				}
			}
		}

		public void FreeObjectArea(GameObject gameObject, float clearanceDistance)
		{
			int xBombPos = Convert.ToInt32(Math.Round(gameObject.transform.position.x));
			int zBombPos = Convert.ToInt32(Math.Round(gameObject.transform.position.z));
			FreeGameFieldPositionArea(xBombPos, zBombPos, clearanceDistance);

			
		}
	}
}
