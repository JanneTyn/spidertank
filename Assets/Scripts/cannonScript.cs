using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cannonScript : MonoBehaviour
{

    public int baseDamage = 70;
    private int trueDamage = 70;
    public int minimumDamage = 5;
    public float perShotDelay = 5f;
    public float bulletSpread = 0.01f;
    public float cannonRange = 20;
    public float explosionRange = 7;
    public float explosionMaxDamageRange = 1;
    public float amplitudeGain = 0.1f;
    public float frequencyGain = 0.1f;
    public float shakeDuration = 0.1f;
    public int side = 0;
    private int finalDamage = 0;
    private int distancedDamage = 0;
    private float timestamp = 0.0f;
    private int layerMask = 1 << 7;
    private int layerMaskTerrain = 1 << 6;
    private Ray crosshairRay;
    private Vector3 dmgUI;
    private GameObject shotEnemy;
    public CrosshairRaycast crosshair;
    public ThirdPersonCam cam;
    public GameObject cannonSpherePrefab;

    [SerializeField] CannonEffect cannonEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(side))
        {
            //StartCoroutine(cam.Shake(amplitudeGain, frequencyGain, shakeDuration));
            if (Time.time > timestamp)
            {
                timestamp = Time.time + perShotDelay;

                trueDamage = Mathf.RoundToInt(baseDamage * (1 + (PlayerStats.playerDamage / 100)));

                //enemiesHit = new List<GameObject>();
                //shotEnemy = crosshair.checkEnemyRaycast(out hit, bulletSpread);
                crosshairRay = crosshair.GetRay();
                GameObject cannonSphere = Instantiate(cannonSpherePrefab, dmgUI, Quaternion.identity);

                if (Physics.Raycast(crosshairRay, out RaycastHit hit2, cannonRange, layerMask) || Physics.Raycast(crosshairRay, out hit2, cannonRange, layerMaskTerrain))
                {                  
                    cannonSphere.transform.position = hit2.point;
                    
                }
                else
                {
                    cannonSphere.transform.position = crosshairRay.GetPoint(cannonRange);
                }

                cannonEffect.CreateLine(cannonSphere.transform.position);

                cannonSphere.transform.localScale = new Vector3(explosionRange, explosionRange, explosionRange);
                cannonSphere.SetActive(true);
                Collider[] hitColliders = Physics.OverlapSphere(cannonSphere.transform.position, explosionRange / 2, layerMask);

                if (hitColliders != null)
                {
                    GetEnemiesInRange(hitColliders, cannonSphere.transform.position);
                }
                else { Debug.Log("Didn't hit anything"); }

                StartCoroutine(DestroyCannonSphere(cannonSphere, 3));

                /*if (crosshair.checkEnemyRaycast(out RaycastHit hit, bulletSpread))
                    {
                        shotEnemy = hit.collider.gameObject;
                        //vihuun osuttu, v‰hennet‰‰n healthia
                        Debug.Log("Enemy hit");

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
                */
            }
        }
        else { cam.endShake(); }
    }

    public void GetEnemiesInRange(Collider[] hitColliders, Vector3 explosionPos)
    {
        foreach (var hitCollider in hitColliders)
        {
            shotEnemy = hitCollider.gameObject;
            //vihuun osuttu, v‰hennet‰‰n healthia
            Debug.Log("Enemy hit");

            if (shotEnemy.gameObject.tag == "MeleeEnemy")
            {
                Enemy enemyScript = GetEnemyParentScript(shotEnemy);
                if (enemyScript != null)
                {
                    finalDamage = CalculateDamageByDistance(shotEnemy.transform.position, explosionPos);
                    enemyScript.TakeDamage(finalDamage);
                    crosshair.createDamageMarker(finalDamage, shotEnemy.transform.position);

                }
                else
                {
                    Debug.Log("ENEMY NULL!!! " + shotEnemy.gameObject);
                }
            }
            else if (shotEnemy.gameObject.tag == "RangeEnemy")
            {
                EnemyRange enemyScriptRange = GetRangeEnemyParentScript(shotEnemy);
                if (enemyScriptRange != null)
                {
                    finalDamage = CalculateDamageByDistance(shotEnemy.transform.position, explosionPos);
                    enemyScriptRange.TakeDamage(finalDamage);
                    crosshair.createDamageMarker(finalDamage, shotEnemy.transform.position);

                }
                else
                {
                    Debug.Log("ENEMY NULL!!! " + shotEnemy.gameObject);
                }
            }
            else
            {
                Debug.Log("Unknown enemy");
            }
        }
    }

    public int CalculateDamageByDistance(Vector3 enemyPos, Vector3 explosionPos)
    {
        float dist = Vector3.Distance(enemyPos, explosionPos);
        Debug.Log(explosionRange + " / " + dist);
        if (dist < explosionMaxDamageRange)
        {
            distancedDamage = trueDamage;
        }
        else
        {
            distancedDamage = (int)Mathf.Lerp(trueDamage, minimumDamage, dist / (explosionRange / 2));
        }
        return distancedDamage;
    }
     
    private IEnumerator DestroyCannonSphere(GameObject sphere, float time)
    {

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(sphere);
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
}
