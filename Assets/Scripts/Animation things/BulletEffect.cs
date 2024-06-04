using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    [SerializeField] CrosshairRaycast crosshair;
    [SerializeField] GameObject effect;
    public Vector3 muzzle;

    public void CreateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(effect, muzzle, Quaternion.LookRotation(direction));
    }
}
