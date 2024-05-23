using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}
