using UnityEngine;
using System.Collections;
using UnityEngine.UI;	
using UnityEngine.SceneManagement;
namespace Completed
{
	public class Player : MovingObject
	{
		public float restartLevelDelay = 1f;		
		public int ppp = 10;				
		public int wallDamage = 1;					
		public Text foodText;													
		private int food;                           

		protected override void Start ()
		{
			food = GameManager.instance.playerhp;
			foodText.text = "HP: " + food;
			base.Start ();
		}

		private void OnDisable ()
		{
			GameManager.instance.playerhp = food;
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

		protected override void AttemptMove <T> (int xDir, int yDir)
		{
			foodText.text = "HP: " + food;
			base.AttemptMove <T> (xDir, yDir);
			RaycastHit2D hit;
			CheckIfGameOver ();
			GameManager.instance.playersTurn = false;
		}
	
		protected override void OnCantMove <T> (T component)
		{
			Wall hitWall = component as Wall;
			hitWall.DamageWall (wallDamage);
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if(other.tag == "Exit")
			{
				Invoke ("Restart", restartLevelDelay);
				enabled = false;
			}
			else if(other.tag == "Food")
			{
				food += ppp;
				foodText.text = "+" + ppp + " HP: " + food;
				other.gameObject.SetActive (false);
			}
		}

		private void Restart ()
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}

		public void LoseFood (int loss)
		{
			food -= loss;
			foodText.text = "-"+ loss + " HP: " + food;
			CheckIfGameOver ();
		}

		private void CheckIfGameOver ()
		{
			if (food <= 0) 
			{
				GameManager.instance.GameOver ();
			}
		}
	}
}

