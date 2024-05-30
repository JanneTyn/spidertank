using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public interface IDamagable
{
    void TakeDamage(int damage);
}

public class Enemy : MonoBehaviour, IDamagable
{
    private NavMeshAgent agent;
    Collider e_Collider;

    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;


    public enum enemystate      
    {
        idle,
        patrol,
        chase,
        attack1,
        attack2

    }

    public enemystate MyState;

    public GameObject Player;
    public Coroutine CurrentBehaviour;

    public float startfollowdistance;
    public float currentdistance;

    public float patrolspeed;
    public float chasespeed;
    public float prepareSpeed;
    public float chargespeed;

    Spawner count;
    

    public int maxEHealth = 100;
    public int curEHealth = 100;

    public int Damage = 10;
    public int experience = 20;
    public int chargeDistance = 5;
    public int chargeTime = 1;
    public PlayerLeveling playerlevel;

    public ParticleSystem blood;
    private bool test = true;

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
            if (trig)
            {
                if (MyState != enemystate.attack1)
                {
                    UpdateBehaviour(enemystate.attack1);
                }
            }
            else if (currentdistance > startfollowdistance && MyState != enemystate.patrol)
            {
                UpdateBehaviour(enemystate.patrol);

            }
            else if (currentdistance <= startfollowdistance && MyState != enemystate.chase)
            {
                UpdateBehaviour(enemystate.chase);

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

            if (currentdistance <= chargeDistance)
            {
                trig = true;
                
            }
            else if (currentdistance >= chargeDistance && MyState != enemystate.attack1)
            {
                trig = false;
            }
        }
    }

    public bool trig;
    

    
   



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
            case enemystate.attack2:
                CurrentBehaviour = StartCoroutine(attack2());
                break;
        }
        
    }

    public void TakeDamage(int damage)
    {
        curEHealth -= damage;

        blood.Play();
        Debug.Log(test);
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
            Vector3 offset = new Vector3(0, 0, 0);

            agent.speed = chasespeed;
            agent.SetDestination(Player.transform.position + offset);
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

        while (true) {

            agent.speed = prepareSpeed;
            agent.SetDestination(Player.transform.position);
            float elapsedTime = 0;

            while (elapsedTime < chargeTime)
            {
                agent.SetDestination(Player.transform.position);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(attack2()); break;
        }
    }


   

   public IEnumerator attack2()
    {
        while (true)
        {
            agent.speed = chargespeed;
            agent.SetDestination(Player.transform.position);
            float elapsedTime = 0;

            while (elapsedTime < chargeTime)
            {
                agent.SetDestination(Player.transform.position);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            StartCoroutine(WaitAfterCharge()); break;
        }
    }

    public IEnumerator WaitAfterCharge()
    {
        agent.speed = 0f;
        float elapsedTime = 0;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        trig = false;
    }

    public void EnemyDeath()
    {

        

        playerlevel.GetEnemyKillExperience(experience);
        Spawner.enemyCount--;
        Destroy(gameObject);
    }
    
    
}

