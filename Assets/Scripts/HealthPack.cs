using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{

    public float rotateSpeed;
    

    private void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed * Time.fixedDeltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Destroy(this.gameObject);
        }
    }
}
