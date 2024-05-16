using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    
   

    private GameObject _player;
    private Vector3 _targetPlayer;
    [SerializeField] private float _speed = 2f;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        _player = GameObject.FindWithTag("Player");
        //null check into get current target location
        if (_player != null)
        {
            _targetPlayer = _player.transform.position;
        }
        else
        {
            Debug.LogError("EnemyShot.player is NULL");
        }
        //calculate direction to move (normalized scales values of vector to be max 1)
        _direction = (_targetPlayer - transform.position + offset).normalized * _speed;

        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            Destroy(this.gameObject);
        }
    }
}
