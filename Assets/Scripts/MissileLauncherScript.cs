using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileLauncherScript : MonoBehaviour
{
    public float missileAmount = 4;
    public float launcherTargetRange = 40;
    public float missileTravelTime = 5;
    public float timeBetweenMissiles = 0.5f;
    public Camera cam;
    public GameObject missilesTarget;
    public GameObject missilePrefab;
    int layerMaskTerrain = 1 << 6;
    bool targeting = false;
    public Vector3 boxTargetArea = new Vector3(0.25F, 0.25F, 0F);
    public Vector3 boxTargetArea2 = new Vector3(0.75F, 0.75F, 0F);
    public Vector3 boxTargetWS; 
    public Vector3 boxTargetWS2;
    public Vector3 boxTargetScale;
    public Vector3 crosshairWS;
    public Vector3 missileStart;
    //public Vector3 missileStartPos;
    public RaycastHit hitInfo;
    Ray ray;
    bool m_HitDetect;

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
        if (Input.GetMouseButton(1))
        {   
            missilesTarget.SetActive(true);
            targeting = true;
            if (Physics.Raycast(ray, out hitInfo, launcherTargetRange, layerMaskTerrain))
            {
                missilesTarget.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.01f, hitInfo.point.z);
            }
            m_HitDetect = Physics.BoxCast(ray.GetPoint(0), boxTargetArea, ray.GetPoint(1), out hitInfo, transform.rotation, launcherTargetRange);
            if (m_HitDetect)
            {
                Debug.Log("boxcast hit something");
            }
        }
        else
        {
            
            if (targeting)
            {
                StartCoroutine(LaunchMissiles());
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
        missile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 missileStartPos = transform.position;
        missile.SetActive(true);
        StartCoroutine(MissileSlerp(missile, missileStartPos, missileTargetPos));
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

        Destroy(firedMissile);
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
