using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

class Spawner : MonoBehaviour
{

	public float timeRemaining = 150;
	public bool timerIsRunning = false;
    public TMPro.TMP_Text timeText;
	


	GameObject[] spawnPoints;
	GameObject currentPoint;

	public GameObject m_EnemyPrefab;
	public GameObject m_EnemyRangePrefab;
	public GameObject m_EnemyExploderPrefab;
	public GameObject demoFin;



	Transform player;


	public int spawnedCount;
	public int aliveEnemyCount;
	public int spawnRange;
	public int waitTime;
	
	int index;


	public static int maxEnemies = 5;
	public static int enemyCount;
	public static int spawnedEnemies;


	private int randomNumber;



	void OnEnable()
	{

	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		//spawning();

	}


	void Update()
	{
		
		MobCheck();
		spawnPoints = GameObject.FindGameObjectsWithTag("Point");

		if (timeRemaining > 0)
		{
			timeRemaining -= Time.deltaTime;
			DisplayTime(timeRemaining);
		}
		else
		{
			Debug.Log("time ran out");
			timeRemaining = 0;
			timerIsRunning = false;
			demoFin.SetActive(true);
			StartCoroutine(ReturnToMenu());
		}

	/*	if (timeRemaining < 120)
        {
			maxEnemies=12;
		}

		if (timeRemaining < 80)
		{
			maxEnemies = 19;
		}

		if (timeRemaining == 0)
		{
			return;
		}
	*/
	}

	void DisplayTime(float timeToDisplay)
	{
		timeToDisplay += 1;
		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);

		timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	void MobCheck()
	{
		aliveEnemyCount = enemyCount;
		spawnedCount = spawnedEnemies;


		if (aliveEnemyCount == 0)
		{
			
			StartCoroutine("WaitAndSpawn");
			
		}

		if (aliveEnemyCount == maxEnemies)
        {
			StopCoroutine("WaitAndSpawn");
        }

	}


	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange, 0.0f, spawnRange));
	}


	private Vector3 GenerateSpawnPos()
	{

		float spawnPosX = Random.Range(-spawnRange, spawnRange);
		float spawnPosZ = Random.Range(-spawnRange, spawnRange);

		randomNumber = Random.Range(0, 4);
		index = Random.Range(0, spawnPoints.Length);
		currentPoint = spawnPoints[index];

		Vector3 spawnPos = currentPoint.transform.position + new Vector3(spawnPosX, 0.91f, spawnPosZ);

		return spawnPos;


	}

	private IEnumerator WaitAndSpawn()
	{
		
		yield return new WaitForSeconds(waitTime);
		Spawn();
		
		

		
	}
	
	void Spawn()
	{
		/*ArrayList gos = new ArrayList();
		gos.AddRange(GameObject.FindGameObjectsWithTag("MeleeEnemy"));
		gos.AddRange(GameObject.FindGameObjectsWithTag("RangeEnemy"));
		gos.AddRange(GameObject.FindGameObjectsWithTag("TankEnemy"));

		int enemyCount = gos.Count;
		*/

		if (enemyCount < maxEnemies)
		{
			enemyCount++;
			spawnedEnemies++;
			
			switch (randomNumber)
			{
				case 0: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); ; break;

				case 1: Instantiate(m_EnemyRangePrefab, GenerateSpawnPos(), m_EnemyRangePrefab.transform.rotation); break;

				case 2: Instantiate(m_EnemyExploderPrefab, GenerateSpawnPos(), m_EnemyExploderPrefab.transform.rotation); break;

				case 3: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); break;
			}
		}
	}

	public IEnumerator ReturnToMenu()
	{
        yield return new WaitForSeconds(5);
		PlayerStats.ResetDefaultValues();
		SceneManager.LoadScene("MainMenu");
    }
}

