using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{

    public int baseDamage = 5;
    public int finalDamage = 0;
    public float perShotDelay = 1f;
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
    public float bulletSpread = 1f;
    public int bullets = 6;
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

                enemiesHit = new List<GameObject>();
                enemiesHit = crosshair.checkEnemyRaycastShotGun(bulletSpread, bullets);
                if (enemiesHit != null)
                {
                    //vihuun osuttu, v‰hennet‰‰n healthia
                    //Debug.Log("Enemy hit");
                    foreach (GameObject enemy in enemiesHit)
                    {
                        Enemy enemyScript = GetEnemyParentScript(enemy);
                        if (enemyScript != null)
                        {
                            //CalculateDamageByDistance(baseDamage, range, shotEnemy);
                            enemyScript.TakeDamage(baseDamage);
                            crosshair.createDamageMarker(baseDamage, enemy);
                        }
                        else
                        {
                            Debug.Log("ENEMY NULL!!!");
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
                shotEnemy = enemyHit.transform.parent.gameObject;
                Enemy enemyscript2 = enemyHit.GetComponent<Enemy>();

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
