using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherScript : MonoBehaviour
{
    public float launcherTargetRange = 40;
    public Camera cam;
    public Vector3 boxTargetArea = new Vector3(0.25F, 0.25F, 0.25F);
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
        if (Input.GetMouseButton(1))
        {       
            m_HitDetect = Physics.BoxCast(ray.GetPoint(0), boxTargetArea, ray.GetPoint(10), out hitInfo, transform.rotation, launcherTargetRange);
            if (m_HitDetect)
            {
                Debug.Log("boxcast hit something");
            }
        }
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
            Gizmos.DrawWireCube(ray.GetPoint(0) + transform.forward * hitInfo.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(ray.GetPoint(0), transform.forward * launcherTargetRange);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(ray.GetPoint(0) + transform.forward * launcherTargetRange, transform.localScale);
        }
    }
}
