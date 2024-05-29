using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileLauncherScript : MonoBehaviour
{
    public int baseDamage = 50;
    private int trueDamage = 50;
    public int minimumDamage = 5;
    public float perShotDelay = 1f;
    public float missileAmount = 4;
    public float explosionRange = 5;
    public float explosionMaxDamageRange = 2;
    public float launcherTargetRange = 40;
    public float missileTravelTime = 5;
    public float timeBetweenMissiles = 0.5f;
    public int side = 0;
    private float groundSlopeAngleX = 0f;
    private float groundSlopeAngleY = 0f;
    private float groundSlopeAngleZ = 0f;
    public Camera cam;
    public GameObject missilesTarget;
    public GameObject missilePrefab;
    public GameObject cannonSpherePrefab;
    public Material targetEnabled;
    public Material targetDisabled;
    bool targetAllowed = false;
    public CrosshairRaycast crosshair;
    private GameObject shotEnemy;
    private float timestamp = 0.0f;
    int layerMaskTerrain = 1 << 6;
    bool targeting = false;
    private int layerMask = 1 << 7;
    private int finalDamage = 0;
    private int distancedDamage = 0;
    public Vector3 boxTargetArea = new Vector3(0.25F, 0.25F, 0F);
    public Vector3 boxTargetArea2 = new Vector3(0.75F, 0.75F, 0F);
    public Vector3 boxTargetWS; 
    public Vector3 boxTargetWS2;
    public Vector3 boxTargetScale;
    public Vector3 crosshairWS;
    public Vector3 missileStart;
    private Vector3 dmgUI;
    //public Vector3 missileStartPos;
    public RaycastHit hitInfo;
    Ray ray;
    bool m_HitDetect;

    public ParticleSystem muzzle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        crosshairWS = cam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
        boxTargetWS = cam.ViewportToWorldPoint(boxTargetArea);
        boxTargetWS2 = cam.ViewportToWorldPoint(boxTargetArea2);
        boxTargetScale = Vector3.Scale(boxTargetWS, boxTargetWS2);
        if (Input.GetMouseButton(side))
        {

            if (Time.time > timestamp)
            {
                trueDamage = Mathf.RoundToInt(baseDamage * (1 + (PlayerStats.playerDamage / 100)));

                targeting = true;
                if (Physics.Raycast(ray, out hitInfo, launcherTargetRange, layerMaskTerrain))
                {
                    missilesTarget.SetActive(true);
                    missilesTarget.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.05f, hitInfo.point.z);
                    if (hitInfo.distance > 5)
                    {                                        
                        missilesTarget.GetComponent<MeshRenderer>().material = targetEnabled;
                        targetAllowed = true;
                    }
                    else
                    {
                        missilesTarget.GetComponent<MeshRenderer>().material = targetDisabled;
                        targetAllowed=false;
                    }

                    //groundSlopeAngleX = Vector3.Angle(hitInfo.normal, Vector3.right);
                    //groundSlopeAngleY = Vector3.Angle(hitInfo.normal, Vector3.up);
                    //groundSlopeAngleZ = Vector3.Angle(hitInfo.normal, Vector3.forward);
                    //missilesTarget.transform.rotation = new Quaternion(groundSlopeAngleX, groundSlopeAngleY, groundSlopeAngleZ, 0);
                }
                m_HitDetect = Physics.BoxCast(ray.GetPoint(0), boxTargetArea, ray.GetPoint(1), out hitInfo, transform.rotation, launcherTargetRange);
                if (m_HitDetect)
                {
                    Debug.Log("boxcast hit something");
                }
            }
        }

        else
        {

            if (targeting)
            {
                if (targetAllowed)
                {
                    StartCoroutine(LaunchMissiles());
                    timestamp = Time.time + perShotDelay;                  
                }
                targeting = false;
            }
            missilesTarget.SetActive(false);
        }
        
    }

    public IEnumerator LaunchMissiles()
    {
        float elapsedTime = 0;
        float timestamp = 0f;
        Vector3 missileTargetArea = missilesTarget.transform.position;

        while (elapsedTime < missileAmount * timeBetweenMissiles)
        {
            if (Time.time > timestamp)
            {
                timestamp = Time.time + timeBetweenMissiles;
                LaunchMissile(missileTargetArea);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void LaunchMissile(Vector3 missileTargetPos)
    {
        GameObject missile = Instantiate(missilePrefab, missileStart, Quaternion.identity);
        missile.transform.SetParent(transform);
        missile.transform.localScale = new Vector3(0.2f, 0.3f, 0.2f);
        Vector3 missileStartPos = transform.position;
        missile.SetActive(true);
        StartCoroutine(MissileSlerp(missile, missileStartPos, missileTargetPos));

        muzzle.Play();

    }

    public IEnumerator MissileSlerp(GameObject firedMissile, Vector3 missileStartpos, Vector3 missileTarget)
    {
        float elapsedTime = 0;
        float startTime = Time.time;

        while (elapsedTime < missileTravelTime)
        {
            Vector3 center = (missileStartpos + missileTarget) * 0.5F;
            center -= new Vector3(0, 1, 0);

            Vector3 startRelCenter = missileStartpos - center;
            Vector3 targetRelCenter = missileTarget - center;

            float fracComplete = (Time.time - startTime) / missileTravelTime;
            firedMissile.transform.position = Vector3.Slerp(startRelCenter, targetRelCenter, fracComplete);
            firedMissile.transform.position += center;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ExplodeMissile(firedMissile);
    }

    public void ExplodeMissile(GameObject firedMissile)
    {
        GameObject cannonSphere = Instantiate(cannonSpherePrefab, dmgUI, Quaternion.identity);

        cannonSphere.transform.position = firedMissile.transform.position;
        cannonSphere.transform.localScale = new Vector3(explosionRange, explosionRange, explosionRange);

        Destroy(firedMissile);

        Collider[] hitColliders = Physics.OverlapSphere(cannonSphere.transform.position, explosionRange / 2, layerMask);

        if (hitColliders != null)
        {
            GetEnemiesInRange(hitColliders, cannonSphere.transform.position);
        }

        StartCoroutine(DestroyCannonSphere(cannonSphere, 3));
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

    public void GetEnemiesInRange(Collider[] hitColliders, Vector3 explosionPos)
    {
        foreach (var hitCollider in hitColliders)
        {
            shotEnemy = hitCollider.gameObject;
            //vihuun osuttu, vähennetään healthia
            Debug.Log("Enemy hit");

            if (shotEnemy.TryGetComponent(out Enemy enemyScript))
            {
                enemyScript = GetEnemyParentScript();
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
            else if (shotEnemy.TryGetComponent(out EnemyRange enemyScriptRange))
            {
                enemyScriptRange = GetRangeEnemyParentScript();
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
            else if (shotEnemy.TryGetComponent(out EnemyExploder enemyScriptExploder))
            {
                enemyScriptExploder = GetExploderEnemyParentScript();
                if (enemyScriptExploder != null)
                {
                    finalDamage = CalculateDamageByDistance(shotEnemy.transform.position, explosionPos);
                    enemyScriptExploder.TakeDamage(finalDamage);
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


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(ray.GetPoint(0), transform.forward * hitInfo.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(crosshairWS + transform.forward * hitInfo.distance, boxTargetScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(ray.GetPoint(0), transform.forward * launcherTargetRange);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(crosshairWS + transform.forward * launcherTargetRange, transform.localScale);
        }
    }
}
