using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyRange : MonoBehaviour
{
    private NavMeshAgent agent;
    Collider e_Collider;
    private Animator anim;
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    

    public enum enemystate
    {
        idle,
        patrol,
        chase,
        attack1
        

    }

    public enemystate MyState;

    public GameObject projectile;
    public float fireDelay;
    
    public GameObject Player;
    public Coroutine CurrentBehaviour;

    public float startfollowdistance;
    public float currentdistance;

    public float patrolspeed;
    public float chasespeed;

    Spawner count;
    


    public int maxEHealth = 100;
    public int curEHealth = 100;

    public int Damage = 10;
    public int experience = 20;
    public PlayerLeveling playerlevel;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
       
        Player = GameObject.FindWithTag("Player");
        playerlevel = Player.GetComponent<PlayerLeveling>();
        e_Collider = GetComponent<Collider>();
        MyState = enemystate.idle;
        StartCoroutine("Mercy");

        count = GameObject.Find("SpawnManager").GetComponent<Spawner>();
        
    }

    private void Update()
    {
        currentdistance = Vector3.Distance(Player.transform.position, transform.position);

        if (this.gameObject != null)
        {
            if (currentdistance > startfollowdistance && MyState != enemystate.patrol)
            {
                UpdateBehaviour(enemystate.patrol);

            }
            else if (currentdistance <= startfollowdistance && MyState != enemystate.chase)
            {
                UpdateBehaviour(enemystate.chase);

            }

            if (trig)
            {
                UpdateBehaviour(enemystate.attack1);
            }
           

            if (curEHealth <= 99)
            {

                startfollowdistance = 300;
            }

            if (curEHealth <= 0)
            {
                
                StopAllCoroutines();
                EnemyDeath();
            }

            if (currentdistance <= 40f)
            {
                trig = true;
                StartCoroutine("attack1");
            }

            if (currentdistance >= 40f)
            {
                agent.speed = 4f;
            }

        }
    }

    public bool trig;
    public bool att;

    private void UpdateBehaviour(enemystate state)
    {

        MyState = state;
        if (CurrentBehaviour != null)
        {
            StopCoroutine(CurrentBehaviour);
        }
        switch (MyState)
        {
            case enemystate.idle:
                CurrentBehaviour = StartCoroutine(idle());
                break;
            case enemystate.patrol:
                CurrentBehaviour = StartCoroutine(patrol());
                break;
            case enemystate.chase:
                CurrentBehaviour = StartCoroutine(chase());
                break;
            case enemystate.attack1:
                CurrentBehaviour = StartCoroutine(attack1());
                break;
            
        }

    }

    public void TakeDamage(int damage)
    {
        curEHealth -= damage;
    }

    public IEnumerator Mercy()
    {
        yield return new WaitForSeconds(25);
        startfollowdistance = 300;
        StopCoroutine("Mercy");
    }

    

    public IEnumerator idle()
    {
        while (true)
        {
            agent.speed = 0;
            yield return null;
        }
    }


    public IEnumerator chase()
    {
        while (true)
        {
            agent.speed = chasespeed;
            agent.SetDestination(Player.transform.position);
            yield return null;

        }
    }

    public IEnumerator patrol()
    {
        while (true)
        {
            UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

            agent.speed = patrolspeed;
            Vector3 target = Random.insideUnitSphere * 12f;
            target += agent.transform.position;
            agent.SetDestination(target);

            yield return null;
        }
    }

    public IEnumerator attack1()
    {
        
        Vector3 offset = new Vector3(0, 3, 0);
        startfollowdistance = 300;
        agent.speed = 1;
        
        yield return new WaitForSeconds(fireDelay);
        
        Instantiate(projectile, transform.position + offset, Quaternion.identity);
        StopCoroutine("attack1");

    }




   

    public void EnemyDeath()
    {

        
        playerlevel.GetEnemyKillExperience(experience);
        Spawner.enemyCount--;
        Destroy(this.gameObject);
    }


}

