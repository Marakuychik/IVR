using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public static GameManager inst = null;
	public Completed.BoardManager boardScript;
	public int playerHP = 100;
	[HideInInspector] public bool playerTurn = true;

//	private Text leveltext;
//	private GameObject LI;
	private int level = 1;
	private bool doingSetup;

	void Awake ()
	{
		if (inst == null)
			inst = this;
		else if (inst != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<Completed.BoardManager> ();
		InitGame ();
	}

	private void OLWL (int index)
	{
		level++;
		InitGame ();
	}

	void InitGame()
	{
		doingSetup = true;
		//LI = GameObject.Find ("LI");
		//leveltext = GameObject.Find ("LevelText").GetComponent<Text>();
		//leveltext.text ="Floor" + level;
		//LI.SetActive (true);
		//Invoke ("HLI", levelStartDelay);
		boardScript.SetupScene (level);
	}

	//private void HLI()
	//{
	//	LI.SetActive (false);
	//	doingSetup = false;
	//
	//}

	public void GO()
	{
		//leveltext.text = "You were killed on the" + level + "floor";
		//LI.SetActive (true);
		enabled = false;
	}

	void Update () 
	{

	}
}
