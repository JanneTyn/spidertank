using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    [SerializeField] Transform target;
    Transform targetPos;

    Quaternion offset;

    private void Awake()
    {
        offset = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z), Vector3.up);

        transform.rotation = transform.rotation * offset;
    }
}
