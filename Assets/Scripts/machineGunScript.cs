using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class machineGunScript : MonoBehaviour
{
    public int baseDamage = 10;
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
                    //Debug.Log("Enemy hit");
                    Enemy enemyScript = GetEnemyParentScript();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage(baseDamage);
                        crosshair.createDamageMarker(baseDamage);
                    }
                    else
                    {
                        Debug.Log("ENEMY NULL!!!");
                    }                  
                }
            }
        }
        else { cam.endShake(); }
    }

    Enemy GetEnemyParentScript()
    {
        Enemy enemyscript = shotEnemy.GetComponent<Enemy>();

        if (enemyscript != null) 
        {
            return enemyscript;
        }
        else
        {
            if (shotEnemy.transform.parent != null)
            {
                shotEnemy = shotEnemy.transform.parent.gameObject;
                Enemy enemyscript2 = shotEnemy.GetComponent<Enemy>();

                if (enemyscript2 != null)
                {
                    return enemyscript2;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }

    
}
