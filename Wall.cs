using UnityEngine;
using System.Collections;

namespace Completed
{
	public class Wall : MonoBehaviour
	{							
		public int hp = 3;							

		private SpriteRenderer spriteRenderer;		

		void Awake ()
		{
			spriteRenderer = GetComponent<SpriteRenderer> ();
		}

		public void DamageWall (int loss)
		{
			hp -= loss;
			if(hp <= 0)
				gameObject.SetActive (false);
		}
	}
}
