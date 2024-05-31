using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTag : MonoBehaviour
{
	public float startspawndis;
	public float currentdis;
	public float tooclosedis;

	Transform player;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Update is called once per frame
	void Update()
	{
		currentdis = Vector3.Distance(player.transform.position, transform.position);

		if (this.gameObject != null)
		{
			if (currentdis > startspawndis)
			{

				gameObject.tag = "Untagged";
			}
			else if (currentdis < startspawndis)
			{

				gameObject.tag = "Point";
			}
			if (currentdis <= tooclosedis)
			{

				gameObject.tag = "Untagged";

			}



		}
	}
}