using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairRaycast : MonoBehaviour
{
    Camera cam;
    Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);


    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
    }
}
