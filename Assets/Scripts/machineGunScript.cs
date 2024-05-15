using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class machineGunScript : MonoBehaviour
{
    public int baseDamage = 10;
    public float perShotDelay = 0.25f;
    public float bulletSpread = 0.03f;
    float shotsfired = 0;
    public ThirdPersonCam cam;
    public CrosshairRaycast crosshair;
    private GameObject shotEnemy;
    public float amplitudeGain = 0.1f;
    public float frequencyGain = 0.1f;
    public float shakeDuration = 0.1f;
    

    private float timestamp = 0.0f;
    
    AudioSource shootingsound;
    public AudioClip clip;
    bool shootingsoundtoggle = false;

    // Start is called before the first frame update
    void Start()
    {
        //cam = GameObject.Find("FreeLookCamera").GetComponent<ThirdPersonCam>();
        shootingsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(cam.Shake(amplitudeGain, frequencyGain, shakeDuration));
            crosshair.machinegunShooting = true;
            if (Time.time > timestamp)
            {
                timestamp = Time.time + perShotDelay;
                shotsfired++;
                Debug.Log("Pam " + shotsfired);
                
                if (!shootingsound.isPlaying); {
                    shootingsound.Play();
                    shootingsoundtoggle = true;
                    Debug.Log("shootingsound is false");
                }

                //shotEnemy = crosshair.checkEnemyRaycast(bulletSpread);
                if (crosshair.checkEnemyRaycast(out RaycastHit hit, bulletSpread))
                {
                    shotEnemy = hit.collider.gameObject;
                    //vihuun osuttu, vähennetään healthia
                    //Debug.Log("Enemy hit");
                    Enemy enemyScript = GetEnemyParentScript();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage(baseDamage);
                        crosshair.createDamageMarker(baseDamage, hit.point);
                    }
                    else
                    {
                        Debug.Log("ENEMY NULL!!!");
                    }                  
                }
            }
        }
        else {
            crosshair.machinegunShooting=false;
            cam.endShake();
            shootingsound.Stop();
            if (shootingsoundtoggle == true) {
                shootingsound.PlayOneShot(clip);
                shootingsoundtoggle = false;
            }
        }
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
