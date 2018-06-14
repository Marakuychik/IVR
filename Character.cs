using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;      


public class Player : MovingObject
{
    private Animator an;                  
	private int health;                           



	protected override void Start ()
	{
		
		an = GetComponent<Animator>();

		health = GameManager.instance.playerhealth;

		base.Start ();
	}


	private void OnDisable ()
	{
		GameManager.instance.playerhealth = health;
	}


	private void Update ()
	{
		if(!GameManager.instance.playersTurn) return;

		int horizontal = 0;     
		int vertical = 0;       



		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));


		vertical = (int) (Input.GetAxisRaw ("Vertical"));


		if(horizontal != 0)
		{
			vertical = 0;
		}


		if(horizontal != 0 || vertical != 0)
		{
			AttemptMove<Wall> (horizontal, vertical);
		}
	}
	private void Restart ()
	{
		SceneManager.LoadScene (0);
	}
	private void CheckIfGameOver ()
	{
		if (health <= 0) 
		{
			GameManager.instance.GameOver ();
		}
	}
}