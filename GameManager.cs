using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;	
using UnityEngine.UI;	
namespace Completed
{					
	public class GameManager : MonoBehaviour
	{
		public float lsd = 2f;						
		public float td = 0.1f;							
		public int playerhp = 100;						
		public static GameManager instance = null;				
		[HideInInspector] public bool playersTurn = true;		
		private Text lvlText;									
		private GameObject lvlImage;						
		private BoardManager boardScript;						
		private int lvl = 1;								
		private List<Enemy> enemies;							
		private bool enemiesMoving;								
		private bool doingSetup = true;									

		void Awake()
		{
            if (instance == null) instance = this;
			else if (instance != this) Destroy(gameObject);	

			DontDestroyOnLoad(gameObject);
			enemies = new List<Enemy>();
			boardScript = GetComponent<BoardManager>();
			InitGame();
		}

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static public void CallbackInitialization()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
			
        static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            instance.lvl++;
            instance.InitGame();
        }
			
		void InitGame()
		{
			doingSetup = true;
			lvlImage = GameObject.Find("LevelImage");
			lvlText = GameObject.Find("LevelText").GetComponent<Text>();
			lvlText.text = "Floor " + lvl;
			lvlImage.SetActive(true);
			Invoke("HideLevelImage", lsd);
			enemies.Clear();
			boardScript.SetupScene(lvl);
			
		}

		void HideLevelImage()
		{
			lvlImage.SetActive(false);
			doingSetup = false;
		}

		void Update()
		{
			if(playersTurn || enemiesMoving || doingSetup) return;

			StartCoroutine (MoveEnemies ());
		}

		public void AddEnemyToList(Enemy script)
		{
			enemies.Add(script);
		}

		public void GameOver()
		{
			lvlText.text = "On " + lvl + " floor you were killed";
			lvlImage.SetActive(true);
			enabled = false;
		}

		IEnumerator MoveEnemies()
		{
			enemiesMoving = true;
			yield return new WaitForSeconds(td);

			if (enemies.Count == 0) 
			{
				yield return new WaitForSeconds(td);
			}

			for (int i = 0; i < enemies.Count; i++)
			{
				enemies[i].MoveEnemy ();
				yield return new WaitForSeconds(enemies[i].moveTime);
			}

			playersTurn = true;
			enemiesMoving = false;
		}
	}
}

