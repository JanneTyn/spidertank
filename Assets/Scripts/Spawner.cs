using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Spawner : MonoBehaviour
{


	//public Transform[] m_SpawnPoints;
	public GameObject m_EnemyPrefab;
	public GameObject m_EnemyRangePrefab;
	
	public Wave wave;

	Transform player;

	int instanceNumber = 1;

	public int aliveEnemyCount;
	public int spawnRange;
	public int waitTime;

	private int randomNumber;
	

	void OnEnable()
    {
		
	}
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		StartCoroutine("WaitAndSpawn");
		//spawning();
	}

    void Update()
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

		Vector3 spawnPos = player.transform.position + new Vector3(spawnPosX, 0.91f, spawnPosZ);

		return spawnPos;
	}


	
	 private IEnumerator WaitAndSpawn()
	{

		yield return new WaitForSeconds(waitTime);
		Spawn();


		StopCoroutine("WaitAndSpawn");
	}

	
	 void Spawn()
	{
		

		


		for (int i = 0; i < wave.enemyCount; i++)
		{
			randomNumber = Random.Range(0, 3);

			switch (randomNumber)
			{
				case 0: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); break;

				case 1: Instantiate(m_EnemyRangePrefab, GenerateSpawnPos(), m_EnemyRangePrefab.transform.rotation); break;

				case 2: Instantiate(m_EnemyPrefab, GenerateSpawnPos(), m_EnemyPrefab.transform.rotation); break;
			}
			
		}
		
	}
/*
	void spawning()
	{
		

		for (int i = 0; i < wave.enemyCount; i++)
		{
			int randomNumber = Mathf.RoundToInt(Random.Range(0f, m_SpawnPoints.Length - 1));

			GameObject currentEntity = Instantiate(m_EnemyPrefab, m_SpawnPoints[randomNumber].transform.position, Quaternion.identity);

			

			

			currentEntity.name = wave.prefabName + instanceNumber;
			instanceNumber++;

		}
	}
*/
}

