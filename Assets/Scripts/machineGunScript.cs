using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineGunScript : MonoBehaviour
{
    public float baseDamage = 10;
    float perShotDelay = 0.25f;
    float shotsfired = 0;

    private float timestamp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > timestamp)
        {
            timestamp = Time.time + perShotDelay;
            shotsfired++;
            Debug.Log("Pam " + shotsfired);

        }
    }
}
