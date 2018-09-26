using UnityEngine;
using System;
using System.Collections.Generic; 		
using Random = UnityEngine.Random; 		

namespace Completed
{
	public class BoardManager : MonoBehaviour
	{
		[Serializable]
		public class Count
		{
			public int min; 			
			public int max; 			
			
			public Count (int minimum, int maximum)
			{
				min = minimum;
				max = maximum;
			}
		}
		
		public int col = 8; 										
		public int row = 8;											
		public Count WallCount = new Count (5, 9);						
		public Count HpCount = new Count (1, 5);						
		public GameObject exit;											
		public GameObject[] FloorTiles;									
		public GameObject[] WallTiles;									
		public GameObject[] HpTiles;									
		public GameObject[] EnemyTiles;									
		public GameObject[] OWallTiles;							
		
		private Transform boardHolder;									
		private List <Vector3> gridPositions = new List <Vector3> ();	
		
		void InitialiseList ()
		{			
			gridPositions.Clear ();

			for(int i = 1; i < col-1; i++)
			{				
				for(int j = 1; j < row-1; j++)
				{					
					gridPositions.Add (new Vector3(i, j, 0f));
				}
			}
		}

		void BoardSetup ()
		{			
			boardHolder = new GameObject ("Board").transform;

			for(int i = -1; i < col + 1; i++)
			{
				for(int j = -1; j < row + 1; j++)
				{
					GameObject toInstantiate = FloorTiles[Random.Range (0,FloorTiles.Length)];

					if(i == -1 || i == col || j == -1 || j == row)
						toInstantiate = OWallTiles [Random.Range (0, OWallTiles.Length)];
					
					GameObject instance = Instantiate (toInstantiate, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
				}
			}
		}

		Vector3 RandomPosition ()
		{
			int randomIndex = Random.Range (0, gridPositions.Count);
			Vector3 randomPosition = gridPositions[randomIndex];
			gridPositions.RemoveAt (randomIndex);
			return randomPosition;
		}

		void LayoutObjectAtRandom (GameObject[] tileArray, int min, int max)
		{
			int objectCount = Random.Range (min, max + 1);

			for(int i = 0; i < objectCount; i++)
			{
				Vector3 randomPosition = RandomPosition();
				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
		}

		public void SetupScene (int level)
		{
			BoardSetup ();
			InitialiseList ();
			LayoutObjectAtRandom (WallTiles, WallCount.min, WallCount.max);
			LayoutObjectAtRandom (HpTiles, HpCount.min, HpCount.max);
			int enemyCount = (int)Mathf.Log(level, 2f);
			LayoutObjectAtRandom (EnemyTiles, enemyCount, enemyCount);
			Instantiate (exit, new Vector3 (col - 1, row - 1, 0f), Quaternion.identity);
		}
	}
}
