using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyExploder : MonoBehaviour, IDamagable
{
    private NavMeshAgent agent;
    Collider m_Collider;

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



    public int maxHealth = 100;
    public int curHealth = 100;

    public int Damage = 10;
    public int experience = 20;
    public PlayerLeveling playerlevel;

    public ParticleSystem blood;
    public ParticleSystem explosion;
    private bool test = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {

        Player = GameObject.FindWithTag("Player");
        playerlevel = Player.GetComponent<PlayerLeveling>();
        m_Collider = GetComponent<Collider>(); 
        MyState = enemystate.idle;
        StartCoroutine("Mercy");

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
            

            if (curHealth <= 99)
            {

                startfollowdistance = 300;
            }

            if (curHealth <= 0)
            {
                StopAllCoroutines();
                EnemyDeath();
            }

            if (currentdistance <= 3f)
            {
                trig = true;
                StartCoroutine("attwait");
            }
            else if (currentdistance >= 3f)
            {
                trig = false;
                att = false;
                StopCoroutine("attwait");
            }
        }
    }

    public bool trig;
    public bool att;

    /*void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            trig = true;
            StartCoroutine(attwait());
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            trig = false;
            att = false;
            StopCoroutine(attwait());
        }
    }
*/
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
        curHealth -= damage;

        blood.Play();
        Debug.Log(test);
    }

    public IEnumerator Mercy()
    {
        yield return new WaitForSeconds(25);
        startfollowdistance = 300;
        StopCoroutine("Mercy");
    }

    public IEnumerator attwait()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            m_Collider.enabled = !m_Collider.enabled;
            explosion.Play();
            yield return new WaitForSeconds(0.2f);
            
            Destroy(gameObject);


            StopCoroutine("attwait");

        }
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

        
        

            agent.speed = 0.4f;
            yield return null;
            
            

            
        
    }




   

    public void EnemyDeath()
    {


        playerlevel.GetEnemyKillExperience(experience);
        Destroy(this.gameObject);
    }


}