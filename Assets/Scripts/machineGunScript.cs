using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class machineGunScript : MonoBehaviour
{
    public int baseDamage = 10;
    private int trueDamage = 10;
    public float perShotDelay = 0.25f;
    public float bulletSpread = 0.03f;
    float shotsfired = 0;
    public int side = 0;
    public ThirdPersonCam cam;
    public CrosshairRaycast crosshair;
    public CheckEnemyTag checkEnemyTag;
    private GameObject shotEnemy;
    public float amplitudeGain = 0.1f;
    public float frequencyGain = 0.1f;
    public float shakeDuration = 0.1f;

    public ParticleSystem muzzle;

    private float timestamp = 0.0f;

    // AudioSource shootingsound;
    // public AudioClip clip;
    // bool shootingsoundtoggle = false;
    public SoundController sound;

    // Start is called before the first frame update
    void Start()
    {
        //cam = GameObject.Find("FreeLookCamera").GetComponent<ThirdPersonCam>();
        
        // shootingsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (Input.GetMouseButton(side))
        {
            StartCoroutine(cam.Shake(amplitudeGain, frequencyGain, shakeDuration));
            crosshair.machinegunShooting = true;
            if (Time.time > timestamp)
            {
                timestamp = Time.time + perShotDelay;
                shotsfired++;

                //if (!shootingsound.isPlaying); {
                //    shootingsound.Play();
                //    shootingsoundtoggle = true;
                //    Debug.Log("shootingsound is false");
                //}
                sound.MG_Sound_On();

                muzzle.Play();

                //shotEnemy = crosshair.checkEnemyRaycast(bulletSpread);
                if (crosshair.checkEnemyRaycast(out RaycastHit hit, bulletSpread))
                {
                    shotEnemy = hit.collider.gameObject;
                    trueDamage = Mathf.RoundToInt(baseDamage * (1 + (PlayerStats.playerDamage / 100)));
                    //vihuun osuttu, v‰hennet‰‰n healthia
                    if (shotEnemy.TryGetComponent(out Enemy enemyScript))
                    {
                        enemyScript = GetEnemyParentScript();
                        if (enemyScript != null)
                        {
                            enemyScript.TakeDamage(trueDamage);
                            crosshair.createDamageMarker(trueDamage, hit.point);
                        }
                        else
                        {
                            Debug.Log("ENEMY NULL!!!");
                        }
                    }
                    else if (shotEnemy.TryGetComponent(out EnemyRange enemyScriptRange))
                    {
                        enemyScriptRange = GetRangeEnemyParentScript();
                        if (enemyScriptRange != null)
                        {
                            enemyScriptRange.TakeDamage(trueDamage);
                            crosshair.createDamageMarker(trueDamage, hit.point);
                        }
                        else
                        {
                            Debug.Log("ENEMY NULL!!!");
                        }
                    }
                    else if (shotEnemy.TryGetComponent(out EnemyExploder enemyScriptExploder))
                    {
                        enemyScriptExploder = GetExploderEnemyParentScript();
                        if (enemyScriptExploder != null)
                        {
                            enemyScriptExploder.TakeDamage(trueDamage);
                            crosshair.createDamageMarker(trueDamage, hit.point);
                        }
                        else
                        {
                            Debug.Log("ENEMY NULL!!!");
                        }
                    }
                    else
                    {
                        Debug.Log("Unknown enemy");
                    }
                                      
                }
            }
        }
        else {
            crosshair.machinegunShooting=false;
            cam.endShake();

            //shootingsound.Stop();
            //if (shootingsoundtoggle == true) {
            //    shootingsound.PlayOneShot(clip);
            //    shootingsoundtoggle = false;
            //}
            sound.MG_Sound_Off();
        }
    }

    Enemy GetEnemyParentScript()
    {
        for (int i = 0; i < 6; i++)
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
                }
            }
        }
        return null;
    }

    EnemyRange GetRangeEnemyParentScript()
    {
        for (int i = 0; i < 6; i++)
        {
            EnemyRange enemyscript = shotEnemy.GetComponent<EnemyRange>();
            if (enemyscript != null)
            {
                return enemyscript;
            }
            else
            {
                if (shotEnemy.transform.parent != null)
                {
                    shotEnemy = shotEnemy.transform.parent.gameObject;
                }
            }
        }
        return null;     
    }

    EnemyExploder GetExploderEnemyParentScript()
    {
        for (int i = 0; i < 6; i++)
        {
            EnemyExploder enemyscript = shotEnemy.GetComponent<EnemyExploder>();
            if (enemyscript != null)
            {
                return enemyscript;
            }
            else
            {
                if (shotEnemy.transform.parent != null)
                {
                    shotEnemy = shotEnemy.transform.parent.gameObject;
                }
            }
        }
        return null;
    }


}
