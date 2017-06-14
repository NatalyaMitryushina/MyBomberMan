using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System.Collections;

public class SmartEnemyController : CharacterControllerBase
{
	public GameFieldPositionsManager gameFieldPosManager;
	private Point previousPlayerPos;
	private Point playerPos;
	private Point previousSmartEnemyPos;
	private Point smartEnemyPos;
	private List<Point> path;
	AStarSearch pathFinding;

	bool delay = false;

	new void Start()
	{
		base.Start();
		this.gameFieldPosManager = ObjectCreator.GetGameFieldPositionsManager();
		path = new List<Point>();
		pathFinding = new AStarSearch();

		playerPos = GetGameObjectPos(GameObject.FindGameObjectWithTag("Player"));
		smartEnemyPos = GetGameObjectPos(this.gameObject);
		previousPlayerPos = playerPos;
		previousSmartEnemyPos = smartEnemyPos;
		
		CalculatePath();
	}

	protected override void TryMove()
	{
		if (path == null)
		{
			CalculatePath();
		}
		else
		{
			GameObject obj = GameObject.FindGameObjectWithTag("Player");
			if (!delay && path.Count > 1 && obj != null)
			{
				delay = true;
				StartCoroutine(CalcDelay());
			}
		}
	}

	private IEnumerator CalcDelay()
	{
		base.TryMove();
		yield return new WaitForSeconds(base.distance / base.speed);
		
		smartEnemyPos = GetGameObjectPos(this.gameObject);
		if(smartEnemyPos != previousSmartEnemyPos)
		{
			path.RemoveAt(0);
			previousSmartEnemyPos = smartEnemyPos;
		}
		RevisePlayerPos();
		delay = false;
	}

	private void RevisePlayerPos()
	{
		GameObject obj = GameObject.FindGameObjectWithTag("Player");
		if (obj != null) playerPos = GetGameObjectPos(GameObject.FindGameObjectWithTag("Player"));
		if (playerPos != previousPlayerPos)
		{
			CalculatePath();
			previousPlayerPos = playerPos;
		}
	}
	protected override Vector3 GetDirection()
	{
		return PhysicsHelper.GetDirectionByTwoPoints(path[0], path[1]);
	}

	private void CalculatePath()
	{
		path = pathFinding.FindPath(gameFieldPosManager, smartEnemyPos, playerPos);
	} 

	private Point GetGameObjectPos(GameObject obj)
	{
		var p = new Point();
		p.x = Convert.ToInt32(Math.Round(obj.transform.position.x));
		p.z = Convert.ToInt32(Math.Round(obj.transform.position.z));
		return p;
	}
}

