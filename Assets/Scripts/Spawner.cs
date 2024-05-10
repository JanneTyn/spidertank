using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Spawner : MonoBehaviour
{

	public GameObject enemyPrefab;



	public Transform[] spawnPoints;

	public void SpawnRandom()
	{
		Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, enemyPrefab.transform.rotation);
	}

    private void Start()
    {
		StartCoroutine(spawning());
			
    }

	IEnumerator spawning()
    {
		while (true)
        {
			SpawnRandom();
			yield return new WaitForSeconds(10f);

        }
    }
}
