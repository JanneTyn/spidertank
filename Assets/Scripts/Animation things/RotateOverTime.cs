using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation != head.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, head.rotation, speed * Time.deltaTime);
        }
    }
}
