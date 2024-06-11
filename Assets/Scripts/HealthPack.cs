using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float respawnTime;
    public float rotateSpeed;
    public GameObject effect;

    public SoundController sound;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        
        transform.Rotate(0, rotateSpeed * Time.fixedDeltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sound.HealthPackSound();
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            effect.gameObject.SetActive(false);
            StartCoroutine("Reactivate");
        }
    }

    private IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(respawnTime);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        effect.gameObject.SetActive(true);
    }

    

    
    
}
