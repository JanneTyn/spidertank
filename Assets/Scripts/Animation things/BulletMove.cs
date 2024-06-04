using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(LifeTime());
    }

    void Update()
    {
        transform.position += transform.forward * 500 * Time.deltaTime;
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
