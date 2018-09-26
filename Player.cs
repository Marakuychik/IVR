using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;
using UnityEngine.UI;

public class Player : MovingObject
{
	public int wallDamage = 1;
	public int ppp = 30;
	public float restartLevDelay = 1f;
	//public Text HPtext;

	private int HP;

	protected override void Start () 
	{
		HP = GameManager.instance.playerFoodPoints;

		//HPtext.text = "HP   " + HP;

		base.Start ();
	}

	private void OnDis()
	{
		GameManager.instance.playerFoodPoints = HP;
	}

	void Update () 
	{
		if (!GameManager.instance.playersTurn)
			return;

		int hor = 0;
		int vert = 0;

		hor = (int)Input.GetAxisRaw ("Horizontal");
		vert = (int)Input.GetAxisRaw ("Vertical");
		if (hor != 0)
			vert = 0;
		if (hor !=0 || vert !=0)
			AM<Wall> (hor, vert);
	}

	protected override void AM <T> (int xDir, int yDir)
	{
		//HPtext.text = "HP   " + HP;
		base.AM <T> (xDir, yDir);
		RaycastHit2D hit;
		CIGO ();
		GameManager.instance.playersTurn = false;
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Exit") {
			Invoke ("Restart", restartLevDelay);
			enabled = false;
		} 
		else if (other.tag == "Items") 
		{
			HP += ppp;
			//HPtext.text = "HP   " + HP;
			other.gameObject.SetActive (false);
		}
	}

	protected override void OCM <T> (T component)
	{
		Wall hitWall = component as Wall;
		hitWall.DamageWall (wallDamage);
	}

	private void Restart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void LH (int loss)
	{
		HP -= loss;
		//HPtext.text = "HP   " + HP;
		CIGO ();
	}

	private void CIGO()
	{
		if (HP <= 0)
			GameManager.instance.GameOver ();
	}
}
