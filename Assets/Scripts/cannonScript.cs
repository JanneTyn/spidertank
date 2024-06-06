using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    int layerMaskEnemyPart = 1 << 8;
    private Ray crosshairRay;
    private Vector3 dmgUI;
    private GameObject shotEnemy;
    public CrosshairRaycast crosshair;
    public ThirdPersonCam cam;
    public GameObject cannonSpherePrefab;
    private List<GameObject> enemyList = new List<GameObject>();
    public TMP_Text shotDelayText1;
    public TMP_Text shotDelayText2;

    [SerializeField] CannonEffect cannonEffect;

    public ParticleSystem muzzle;

    public SoundController sound; 

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
                timestamp = Time.time + (perShotDelay * (1 / PlayerStats.playerFireRate));

                trueDamage = Mathf.RoundToInt(baseDamage * (1 + (PlayerStats.playerDamage / 100)));
                trueDamage = PlayerStats.RollCriticalChance(trueDamage);

                //enemiesHit = new List<GameObject>();
                //shotEnemy = crosshair.checkEnemyRaycast(out hit, bulletSpread);
                crosshairRay = crosshair.GetRay();
                GameObject cannonSphere = Instantiate(cannonSpherePrefab, dmgUI, Quaternion.identity);

                muzzle.Play();
                sound.CannonSound();

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

                sound.ExplosionSound();

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
        DisplayRemainingShotDelay();
    }

    public void GetEnemiesInRange(Collider[] hitColliders, Vector3 explosionPos)
    {
        foreach (var hitCollider in hitColliders)
        {
            shotEnemy = hitCollider.gameObject;
            //vihuun osuttu, v‰hennet‰‰n healthia
            Debug.Log("Enemy hit");

            if (shotEnemy.TryGetComponent(out Enemy enemyScript))
            {
                enemyScript = GetEnemyParentScript(shotEnemy);
                if (enemyScript != null)
                {
                    if (!enemyList.Contains(shotEnemy))
                    {
                        finalDamage = CalculateDamageByDistance(hitCollider.gameObject.transform.position, explosionPos);
                        enemyScript.TakeDamage(finalDamage);
                        crosshair.createDamageMarker(finalDamage, hitCollider.gameObject.transform.position);
                        enemyList.Add(shotEnemy);
                    }
                    else
                    {
                        Debug.Log("Enemy was already damaged");
                    }

                }
                else
                {
                    Debug.Log("ENEMY NULL!!! " + shotEnemy.gameObject);
                }
            }
            else if (shotEnemy.TryGetComponent(out EnemyRange enemyScriptRange))
            {
                enemyScriptRange = GetRangeEnemyParentScript(shotEnemy);
                if (enemyScriptRange != null)
                {
                    if (!enemyList.Contains(shotEnemy))
                    {
                        finalDamage = CalculateDamageByDistance(hitCollider.gameObject.transform.position, explosionPos);
                        enemyScriptRange.TakeDamage(finalDamage);
                        crosshair.createDamageMarker(finalDamage, hitCollider.gameObject.transform.position);
                        enemyList.Add(shotEnemy);
                    }
                    else
                    {
                        Debug.Log("Enemy was already damaged");
                    }

                }
                else
                {
                    Debug.Log("ENEMY NULL!!! " + shotEnemy.gameObject);
                }
            }
            else if (shotEnemy.TryGetComponent(out EnemyExploder enemyScriptExploder))
            {
                enemyScriptExploder = GetExploderEnemyParentScript(shotEnemy);
                if (enemyScriptExploder != null)
                {
                    if (!enemyList.Contains(shotEnemy))
                    {
                        finalDamage = CalculateDamageByDistance(hitCollider.gameObject.transform.position, explosionPos);
                        enemyScriptExploder.TakeDamage(finalDamage);
                        crosshair.createDamageMarker(finalDamage, hitCollider.gameObject.transform.position);
                        enemyList.Add(shotEnemy);
                    }
                    else
                    {
                        Debug.Log("Enemy was already damaged");
                    }

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
        enemyList.Clear();
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
        for (int i = 0; i < 6; i++)
        {
            Enemy enemyscript = enemyHit.GetComponent<Enemy>();
            if (enemyscript != null)
            {
                shotEnemy = enemyHit;
                return enemyscript;
            }
            else
            {
                if (enemyHit.transform.parent != null)
                {
                    enemyHit = enemyHit.transform.parent.gameObject;
                }
            }
        }
        return null;
    }

    EnemyRange GetRangeEnemyParentScript(GameObject enemyHit)
    {
        for (int i = 0; i < 6; i++)
        {
            EnemyRange enemyscript = enemyHit.GetComponent<EnemyRange>();
            if (enemyscript != null)
            {
                shotEnemy = enemyHit;
                return enemyscript;
            }
            else
            {
                if (enemyHit.transform.parent != null)
                {
                    enemyHit = enemyHit.transform.parent.gameObject;
                }
            }
        }
        return null;     
    }

    EnemyExploder GetExploderEnemyParentScript(GameObject enemyHit)
    {
        for (int i = 0; i < 6; i++)
        {
            EnemyExploder enemyscript = enemyHit.GetComponent<EnemyExploder>();
            if (enemyscript != null)
            {
                shotEnemy = enemyHit;
                return enemyscript;
            }
            else
            {
                if (enemyHit.transform.parent != null)
                {
                    enemyHit = enemyHit.transform.parent.gameObject;
                }
            }
        }
        return null;
    }

    public void DisplayRemainingShotDelay()
    {
        float timeLeft = timestamp - Time.time;
        if (side == 0)
        {
            if (timeLeft < 0)
            {
                shotDelayText1.gameObject.SetActive(false);
            }
            else
            {
                shotDelayText1.gameObject.SetActive(true);
                shotDelayText1.text = timeLeft.ToString("F1");
            }
        }
        else
        {
            if (timeLeft < 0)
            {
                shotDelayText2.gameObject.SetActive(false);
            }
            else
            {
                shotDelayText2.gameObject.SetActive(true);
                shotDelayText2.text = timeLeft.ToString("F1");
            }
        }
    }
}
