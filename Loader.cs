using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;

public class Loader : MonoBehaviour 
{
	public GameObject gameMan;

	void Start () 
	{
		if (GameManager.instance == null)
			Instantiate (gameMan);
	}
}
