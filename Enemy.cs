using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MovingObject
{
	public int playerDamage;

	private Transform target;
	private bool skip;

	protected override void Start () 
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();
	}

	void Update () 
	{
		
	}

	protected override void AM <T> (int xDir, int yDir)
	{
		if (skip)
		{
			skip = false;
			return;
		}
		base.AM <T> (xDir, yDir);
		skip = true;
	}

	public void MoveEnemy()
	{
		int xDir = 0;
		int yDir = 0;
		if (Math.Abs (target.position.x - transform.position.x) < float.Epsilon)
			yDir = target.position.y > transform.position.y ? 1 : -1;
		else
			xDir = target.position.x > transform.position.x ? 1 : -1;
		AM<Player> (xDir, yDir);
	}

	protected override void OCM <T> (T component)
	{
		Player hitPlayer = component as Player;
		hitPlayer.LH (playerDamage);
	}
}
