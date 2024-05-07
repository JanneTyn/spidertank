using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

   


    public enum enemystate      
    {
        idle,
        patrol,
        chase,
        attack

    }

    public enemystate MyState;

    public GameObject Player;
    public Coroutine CurrentBehaviour;

    public float startfollowdistance;
    public float currentdistance;

    

    //public float patrolspeed;
   // public float followspeed;

    

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");

        MyState = enemystate.idle;

    }

    private void Update()
    {
        currentdistance = Vector3.Distance(Player.transform.position, transform.position);

        
        if (currentdistance > startfollowdistance && MyState != enemystate.patrol)
        {
            UpdateBehaviour(enemystate.patrol);
        }
        else if (currentdistance <= startfollowdistance && MyState != enemystate.chase)
        {
            UpdateBehaviour(enemystate.chase);
        }  
    }
    

   

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
            case enemystate.attack:
                CurrentBehaviour = StartCoroutine(attack());
                break;
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
            agent.speed = 10;
            agent.SetDestination(Player.transform.position);
            yield return null;

        }
    }

    public IEnumerator patrol()
    {
        while (true)
        {
            agent.speed = 3.5f;
            Vector3 target = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            target += agent.transform.position;
            agent.SetDestination(target);

            yield return null;
        }
    }

    public IEnumerator attack()
    {
        while (true)
        {
            agent.speed = 0;
            yield return null;
        }
    }
}

