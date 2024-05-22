using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{

    public int baseDamage = 5;
    public int finalDamage = 0;
    public float perShotDelay = 1f;
    public float bulletSpread = 1f;
    public int bullets = 6;
    float shotsfired = 0;
    public float range = 10;
    public ThirdPersonCam cam;
    public CrosshairRaycast crosshair;
    private GameObject shotEnemy;
    private List<GameObject> enemiesHit;
    private float timestamp = 0.0f;
    public float amplitudeGain = 0.1f;
    public float frequencyGain = 0.1f;
    public float shakeDuration = 0.1f;  
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            StartCoroutine(cam.Shake(amplitudeGain, frequencyGain, shakeDuration));
            if (Time.time > timestamp)
            {
                timestamp = Time.time + perShotDelay;
                shotsfired++;
                //Debug.Log("Pam " + shotsfired);

                for (int i = 0; i < bullets; i++) {
                    //enemiesHit = new List<GameObject>();
                    //shotEnemy = crosshair.checkEnemyRaycast(out hit, bulletSpread);
                    if (crosshair.checkEnemyRaycast(out RaycastHit hit, bulletSpread))
                    {
                        shotEnemy = hit.collider.gameObject;
                        //vihuun osuttu, vähennetään healthia
                        Debug.Log("Enemy hit");

                        if (shotEnemy.gameObject.tag == "MeleeEnemy")
                        {
                            Enemy enemyScript = GetEnemyParentScript(shotEnemy);
                            if (enemyScript != null)
                            {
                                //CalculateDamageByDistance(baseDamage, range, shotEnemy);
                                enemyScript.TakeDamage(baseDamage);
                                crosshair.createDamageMarker(baseDamage, hit.point);
                            }
                            else
                            {
                                Debug.Log("ENEMY NULL!!!");
                            }
                        }
                        else if (shotEnemy.gameObject.tag == "RangeEnemy")
                        {
                            EnemyRange enemyScriptRange = GetRangeEnemyParentScript(shotEnemy);
                            if (enemyScriptRange != null)
                            {
                                //CalculateDamageByDistance(baseDamage, range, shotEnemy);
                                enemyScriptRange.TakeDamage(baseDamage);
                                crosshair.createDamageMarker(baseDamage, hit.point);
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
        }       
        else { cam.endShake(); }
    }

    Enemy GetEnemyParentScript(GameObject enemyHit)
    {
        Enemy enemyscript = enemyHit.GetComponent<Enemy>();

        if (enemyscript != null)
        {
            return enemyscript;
        }
        else
        {
            if (enemyHit.transform.parent != null)
            {
                enemyHit = enemyHit.transform.parent.gameObject;
                Enemy enemyscript2 = enemyHit.GetComponent<Enemy>();

                if (enemyscript2 != null)
                {
                    return enemyscript2;
                }
                else
                {
                    if (enemyHit.transform.parent != null)
                    {
                        enemyHit = enemyHit.transform.parent.gameObject;
                        Enemy enemyscript3 = enemyHit.GetComponent<Enemy>();

                        if (enemyscript3 != null)
                        {
                            return enemyscript3;
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
            else
            {
                return null;
            }
        }

    }

    EnemyRange GetRangeEnemyParentScript(GameObject enemyHit)
    {
        EnemyRange enemyscript = enemyHit.GetComponent<EnemyRange>();

        if (enemyscript != null)
        {
            return enemyscript;
        }
        else
        {
            if (enemyHit.transform.parent != null)
            {
                enemyHit = enemyHit.transform.parent.gameObject;
                EnemyRange enemyscript2 = enemyHit.GetComponent<EnemyRange>();

                if (enemyscript2 != null)
                {
                    return enemyscript2;
                }
                else
                {
                    if (enemyHit.transform.parent != null)
                    {
                        enemyHit = enemyHit.transform.parent.gameObject;
                        EnemyRange enemyscript3 = enemyHit.GetComponent<EnemyRange>();

                        if (enemyscript3 != null)
                        {
                            return enemyscript3;
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
            else
            {
                return null;
            }
        }

    }

    void CalculateDamageByDistance(float dmg, float range, GameObject shotenemy)
    {
        float dist = Vector3.Distance(shotenemy.transform.position, this.transform.position);
        if (dist < range) 
        {
            finalDamage = (int)dmg;
        }
        else
        {
            float overDistance = dist - range;

            finalDamage = (int)Mathf.Lerp(2, baseDamage, range / dist);
            if (finalDamage < 2)
            {
                finalDamage = 2;
            }
        }


    }
}
