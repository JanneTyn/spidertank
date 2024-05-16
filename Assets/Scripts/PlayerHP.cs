using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    public int maxHealth = 100;
    public int curHP = 100;

    
    

    public int damage = 5;
    public int damageProjectile = 10;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (curHP != 0)
        {

        }

        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            curHP -= damage;
        }
        if (collision.gameObject.tag == "Projectile")
        {
            curHP -= damageProjectile;
            
        }
    }
}
