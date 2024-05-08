using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class machineGunScript : MonoBehaviour
{
    public float baseDamage = 10;
    float perShotDelay = 0.25f;
    float shotsfired = 0;
    public ThirdPersonCam cam;
    public CrosshairRaycast crosshair;
    private GameObject shotEnemy;

    private float timestamp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //cam = GameObject.Find("FreeLookCamera").GetComponent<ThirdPersonCam>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(cam.Shake());
            if (Time.time > timestamp)
            {
                timestamp = Time.time + perShotDelay;
                shotsfired++;
                Debug.Log("Pam " + shotsfired);

                shotEnemy = crosshair.checkEnemyRaycast();
                if (shotEnemy != null)
                {
                    //vihuun osuttu, v‰hennet‰‰n healthia
                    Debug.Log("Enemy hit");
                    crosshair.createDamageMarker(baseDamage);
                }
            }
        }
        else { cam.endShake(); }
    }

    
}
