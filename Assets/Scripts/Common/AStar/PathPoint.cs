using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Common.AStar
{
	public class PathPoint
	{
		public Point Position { get; set; }
		public PathPoint CameFrom { get; set; }
		public float PathLengthFromStart { get; set; }
		public float HeuristicEstimateToFinish { get; set; }
		public float TotalEstimateFullPathLength
		{
			get
			{
				return this.PathLengthFromStart + this.HeuristicEstimateToFinish;
			}
		}

		public PathPoint(Point position, PathPoint cameFrom, float pathLengthFromStart, float heuristicEstimateToFinish)
		{
			Position = position;
			CameFrom = cameFrom;
			PathLengthFromStart = pathLengthFromStart;
			HeuristicEstimateToFinish = heuristicEstimateToFinish;
		}


	}
}
