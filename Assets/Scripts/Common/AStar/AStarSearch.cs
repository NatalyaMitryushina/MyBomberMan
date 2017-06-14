using Assets.Scripts.Common;
using Assets.Scripts.Common.AStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AStarSearch
{
	private float _weigth = 1f;

	public List<Point> FindPath(GameFieldPositionsManager field, Point start, Point finish)
	{
		var closedSet = new List<PathPoint>();
		var openSet = new List<PathPoint>();
		PathPoint startPoint = new PathPoint(start, null, 0f, GetHeuristic(start, finish));
		openSet.Add(startPoint);

		while(openSet.Count > 0)
		{
			var currentPoint = openSet.OrderBy(point => point.TotalEstimateFullPathLength).First();

			if (currentPoint.Position == finish) return GetPathForPoint(currentPoint);
			openSet.Remove(currentPoint);
			closedSet.Add(currentPoint);

			foreach(var neighbourPos in GetAvailableNeighbourPositions(field, currentPoint.Position))
			{
				var neighbourPoint = new PathPoint(neighbourPos, currentPoint, currentPoint.PathLengthFromStart + _weigth,
					GetHeuristic(neighbourPos, finish));

				if (closedSet.Count(point => point.Position == neighbourPoint.Position) > 0) continue;

				var openPoint = openSet.FirstOrDefault(point => point.Position == neighbourPoint.Position);

				if (openPoint == null) openSet.Add(neighbourPoint);
				else
					if(openPoint.PathLengthFromStart > neighbourPoint.PathLengthFromStart)
					{
						openPoint.CameFrom = currentPoint;
						openPoint.PathLengthFromStart = neighbourPoint.PathLengthFromStart;
					}
			}
		}
		return null;
	}

	public List<Point> GetAvailableNeighbourPositions(GameFieldPositionsManager field, Point current)
	{
		var result = new List<Point>();
		foreach (var dir in PhysicsHelper.characterDirections)
		{
			Point next = new Point();
			next.x = current.x + Convert.ToInt32(dir.x);
			next.z = current.z + Convert.ToInt32(dir.z);

			if ( !field.IsFilled(next.x, next.z)
				   && field.InBounds(next.x, next.z))
				result.Add(next);
		}
		return result;
	}

	private List<Point> GetPathForPoint(PathPoint pathPoint)
	{
		var result = new List<Point>();
		var currentPoint = pathPoint;
		//StringBuilder sb = new StringBuilder();
		//sb.Append("Path: ");
		while (currentPoint != null)
		{
			result.Add(currentPoint.Position);
			//sb.Append("x:" + currentPoint.Position.x.ToString());
			//sb.Append("z:" + currentPoint.Position.z.ToString());
			currentPoint = currentPoint.CameFrom;
		}
		//Debug.Log(sb);
		result.Reverse();
		return result;

	}

	private float GetHeuristic(Point start, Point finish)
	{
		return (float)Math.Sqrt(Math.Pow(start.x - finish.x, 2) + Math.Pow(start.z - finish.z, 2));
	}

}

