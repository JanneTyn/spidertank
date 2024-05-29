using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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



	Transform player;



	public int aliveEnemyCount;
	public int spawnRange;
	public int waitTime;
	int index;


	public static int maxEnemies = 5;
	public static int enemyCount = 0;
	


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
		ArrayList gos = new ArrayList();
		gos.AddRange(GameObject.FindGameObjectsWithTag("MeleeEnemy"));
		gos.AddRange(GameObject.FindGameObjectsWithTag("RangeEnemy"));
		gos.AddRange(GameObject.FindGameObjectsWithTag("TankEnemy"));

		aliveEnemyCount = gos.Count;

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
		}

		if (timeRemaining < 120)
        {
			maxEnemies=12;
		}

		if (timeRemaining < 80)
		{
			maxEnemies = 19;
		}

		if (timeRemaining == 0)
		{		
			StartCoroutine("Ending");
		}

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

		

		if (aliveEnemyCount == 0)
		{

			StartCoroutine("WaitAndSpawn");
			//spawning();
		}

		if (aliveEnemyCount <= maxEnemies)
		{

			StartCoroutine("WaitAndSpawn");
			//spawning();
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

	private IEnumerator Ending()
    {
		yield return new WaitForSeconds(6);
		
		
		
	}

	private IEnumerator WaitAndSpawn()
	{
		Debug.Log("started spawning");
		yield return new WaitForSeconds(waitTime);
		Spawn();
		
		

		
	}
	
	void Spawn()
	{
		ArrayList gos = new ArrayList();
		gos.AddRange(GameObject.FindGameObjectsWithTag("MeleeEnemy"));
		gos.AddRange(GameObject.FindGameObjectsWithTag("RangeEnemy"));
		gos.AddRange(GameObject.FindGameObjectsWithTag("TankEnemy"));

		int enemyCount = gos.Count;
		

		if (enemyCount < maxEnemies)
		{
			enemyCount++;

			
			switch (randomNumber)
			{
				case 0: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); ; break;

				case 1: Instantiate(m_EnemyRangePrefab, GenerateSpawnPos(), m_EnemyRangePrefab.transform.rotation); break;

				case 2: Instantiate(m_EnemyExploderPrefab, GenerateSpawnPos(), m_EnemyExploderPrefab.transform.rotation); break;

				case 3: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); break;


				


			}
		}
	/*	for (int i = 0; i < enemyCount; i++)
		{

			randomNumber = Random.Range(0, 3);

			switch (randomNumber)
			{
				case 0: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); ; break;

				case 1: Instantiate(m_EnemyRangePrefab, GenerateSpawnPos(), m_EnemyRangePrefab.transform.rotation); break;

				case 2: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); break;


			}

		}
	*/
	}
}

