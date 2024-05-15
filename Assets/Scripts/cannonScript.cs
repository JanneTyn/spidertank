using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cannonScript : MonoBehaviour
{

    public int baseDamage = 70;
    public float perShotDelay = 5f;
    public float bulletSpread = 0.01f;
    public float cannonRange = 20;
    public float amplitudeGain = 0.1f;
    public float frequencyGain = 0.1f;
    public float shakeDuration = 0.1f;
    private float timestamp = 0.0f;
    private int layerMask = 1 << 7;
    private int layerMaskTerrain = 1 << 6;
    private Ray crosshairRay;
    private Vector3 dmgUI;
    private GameObject shotEnemy;
    public CrosshairRaycast crosshair;
    public ThirdPersonCam cam;
    public GameObject cannonSpherePrefab;
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

                //enemiesHit = new List<GameObject>();
                //shotEnemy = crosshair.checkEnemyRaycast(out hit, bulletSpread);
                crosshairRay = crosshair.GetRay();

                if (Physics.Raycast(crosshairRay, out RaycastHit hit2, cannonRange, layerMask) || Physics.Raycast(crosshairRay, out hit2, cannonRange, layerMaskTerrain))
                {
                    GameObject cannonSphere = Instantiate(cannonSpherePrefab, dmgUI, Quaternion.identity);
                    cannonSphere.transform.position = hit2.point;
                    cannonSphere.SetActive(true);
                    StartCoroutine(DestroyCannonSphere(cannonSphere, 3));
                }
                    if (crosshair.checkEnemyRaycast(out RaycastHit hit, bulletSpread))
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
                
            }
        }
        else { cam.endShake(); }
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
}
