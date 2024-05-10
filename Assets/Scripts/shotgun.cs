using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{

    public int baseDamage = 30;
    public int finalDamage = 0;
    public float perShotDelay = 1f;
    float shotsfired = 0;
    public float range = 10;
    public ThirdPersonCam cam;
    public CrosshairRaycast crosshair;
    private GameObject shotEnemy;
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

                shotEnemy = crosshair.checkEnemyRaycast();
                if (shotEnemy != null)
                {
                    //vihuun osuttu, v‰hennet‰‰n healthia
                    //Debug.Log("Enemy hit");
                    Enemy enemyScript = GetEnemyParentScript();
                    if (enemyScript != null)
                    {
                        CalculateDamageByDistance(baseDamage, range, shotEnemy);
                        enemyScript.TakeDamage(finalDamage);
                        crosshair.createDamageMarker(finalDamage);
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

            finalDamage = (int)Mathf.Lerp(2, baseDamage, overDistance / dist);
            if (finalDamage < 2)
            {
                finalDamage = 2;
            }
        }


    }
}
