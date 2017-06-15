using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Base;
using System.Linq;

public class PlayerController : CharacterControllerBase
{
	private List<Powerup> powerups = new List<Powerup>();
	public GameObject Bomb { get; set; }
	public float Distance { get; set; }
	private List<GameObject> droppedBombs = new List<GameObject>();
	private GameFieldPositionsManager gameFieldPosManager;


	public PlayerController()
	{
		this.gameFieldPosManager = ObjectCreator.GetGameFieldPositionsManager();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (CanDropBomb())
			{
				DropBomb();
			}
		}
	}

	protected override Vector3 GetDirection()
	{
		return PhysicsHelper.GetDirectionByKey();
	}

	protected override void TryMove()
	{
		CheckSpeed();
		base.TryMove();
	}

	private void CheckSpeed()
	{
		if (powerups.Exists(p => p.powerupType == PowerupType.Speed)) this.speed = PhysicsHelper.CharacterSpeed[SpeedType.Fast];
	}

	protected override bool CanGo(Vector3 nextPoint)
	{
		if (powerups.Exists(p => p.powerupType == PowerupType.Wallpass))
		{
			var hitColliders = Physics.OverlapCapsule(nextPoint, nextPoint, 0.5f);
			foreach (var col in hitColliders)
			{
				if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy") return false;
			}
			return true;
		}
		else
		{
			return base.CanGo(nextPoint);
		}

	}
	private bool CanDropBomb()
	{
		int allowedBombs = this.powerups.Count(p => p.powerupType == PowerupType.Bombs);
		return droppedBombs.Count <= allowedBombs;
	}

	private void DropBomb()
	{
		Vector3 position = new Vector3(transform.position.x, transform.position.y - 0.9f, transform.position.z);
		InstallExplosionDistance();
		GameObject bomb = Instantiate(Bomb, position, Quaternion.identity);
		droppedBombs.Add(bomb);
		StartCoroutine(ExplodeBomb(bomb));
	}

	private void InstallExplosionDistance()
	{
		Distance = powerups.Count(p => p.powerupType == PowerupType.Flames);		
	}
	private IEnumerator ExplodeBomb(GameObject bombObject)
	{
		yield return new WaitForSeconds(Bomb.GetComponent<BombControllerBase>().ActionTime);

		gameFieldPosManager.FreeObjectArea(bombObject, Bomb.GetComponent<BombControllerBase>().ExplosionDistance);
		droppedBombs.Remove(bombObject);
		UnityEngine.Object.DestroyObject(bombObject);
	}

	

	void OnTriggerEnter(Collider other)
	{
		switch(other.tag)
		{
			case "Enemy":
				DestroyObject(this.gameObject);
				break;
			case "Powerup":
				other.gameObject.SetActive(false);
				powerups.Add(other.gameObject.GetComponent<Powerup>());
				break;
		}
	}

	

}