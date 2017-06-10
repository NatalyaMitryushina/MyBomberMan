using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Common
{
	public class GameFieldPositionsManager
	{
		private struct GameFieldPosition
		{
			public bool isFilled;
			public string objectType;
		};

		private GameFieldPosition[,] gameFieldPositions;
		private int zCount;
		private int xCount;

		public GameFieldPositionsManager(int xCount, int zCount)
		{
			gameFieldPositions = new GameFieldPosition[zCount, xCount];
			this.zCount = zCount;
			this.xCount = xCount;
		}

		public void FreeGameFieldPositionArea(int indexX, int indexZ, float radius)
		{
			int fieldPosCount;
			if (radius < 1f) fieldPosCount = 1;
			else fieldPosCount = (int)Math.Round(radius);

			for (var i = 1; i <= fieldPosCount; i++)
			{
				if (indexX - i >= 0) FreeGameFieldPosition(indexX - i, indexZ);
				if (indexX + i < xCount) FreeGameFieldPosition(indexX + i, indexZ);
				if (indexZ - i >= 0) FreeGameFieldPosition(indexX, indexZ - i);
				if (indexZ + i < zCount) FreeGameFieldPosition(indexX, indexZ + i);
			}
		}

		public void FillGameFieldPosition(int indexX, int indexZ, string objectTag)
		{
			gameFieldPositions[indexZ, indexX].isFilled = true;
			gameFieldPositions[indexZ, indexX].objectType = objectTag;
		}

		public void FreeGameFieldPosition(int indexX, int indexZ)
		{
			gameFieldPositions[indexZ, indexX].isFilled = false;
			gameFieldPositions[indexZ, indexX].objectType = "";
		}

		public bool IsFilled(int indexX, int indexZ)
		{
			if (gameFieldPositions[indexZ, indexX].isFilled) return true;
			return false;
		}
	}
}
