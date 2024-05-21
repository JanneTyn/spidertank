using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class Spawner : MonoBehaviour
{



	public GameObject m_EnemyPrefab;
	public GameObject m_EnemyRangePrefab;



	Transform player;



	public int aliveEnemyCount;
	public int spawnRange;
	public int waitTime;

	public static int maxEnemies = 5;
	public static int enemyCount = 0;
	


	private int randomNumber;


	public float startspawndis;
	public float currentdis;





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

		

		currentdis = Vector3.Distance(player.transform.position, transform.position);

		if (this.gameObject != null)
		{
			if (currentdis > startspawndis)
			{
				StopCoroutine("WaitAndSpawn");

			}
			else if (currentdis < startspawndis)
			{

				MobCheck();
			}
			
		}



	}

	void MobCheck()
	{

		aliveEnemyCount = FindObjectsOfType<Enemy>().Length;
		GameObject[] gameObjects;
		gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

		if (gameObjects.Length == 0)
		{

			StartCoroutine("WaitAndSpawn");
			//spawning();
		}
	}

	private Vector3 GenerateSpawnPos()
	{

		float spawnPosX = Random.Range(-spawnRange, spawnRange);
		float spawnPosZ = Random.Range(-spawnRange, spawnRange);

		Vector3 spawnPos = transform.position + new Vector3(spawnPosX, 0.91f, spawnPosZ);

		return spawnPos;


	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange, 0.0f, spawnRange));
	}



	private IEnumerator WaitAndSpawn()
	{
		Debug.Log("started spawning");
		yield return new WaitForSeconds(waitTime);
		Spawn();
		//enemyCount += 2;
		

		
	}

	
	void Spawn()
	{
		int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;


		if (enemyCount < maxEnemies)
		{
			enemyCount++;
			randomNumber = Random.Range(0, 3);

			switch (randomNumber)
			{
				case 0: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); ; break;

				case 1: Instantiate(m_EnemyRangePrefab, GenerateSpawnPos(), m_EnemyRangePrefab.transform.rotation); break;

				case 2: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); break;


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

