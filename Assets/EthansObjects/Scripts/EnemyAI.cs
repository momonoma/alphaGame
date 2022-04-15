using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    public float attackDelay = 2f;
    public int enemyDamage = 10;
    public float attackDistance = 2f;
    public Animator anim;
    public Transform eyes;
    public float fieldOfView = 45f;
    public float sightDistance = 10f;
    public PlayerBehavior ph;

    float attackTimer = 0f;

    public enum FSMstate
    {
        IDLE,
        CHASE,
        ATTACK,
        DEAD
    }

    public FSMstate currentState;

    float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = FSMstate.IDLE;
        //ph = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        eyes = transform.Find("EnemyEyes");
        ph = player.GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case FSMstate.IDLE:
                Idle();
                break;
            case FSMstate.CHASE:
                Chase();
                break;
            case FSMstate.ATTACK:
                Attack();
                break;
            case FSMstate.DEAD:
                Die();
                break;
        }
    }

    void Idle()
    {
        anim.SetInteger("animState", 0);

        agent.isStopped = true;
        if(SightAlert())
        {
            currentState = FSMstate.CHASE;
        }
    }

    void Chase()
    {
        if(playerDistance > attackDistance)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }

        anim.SetInteger("animState", 1);
        agent.SetDestination(player.transform.position);

        if(playerDistance <= attackDistance)
        {
            currentState = FSMstate.ATTACK;
        }
        else
        {
            currentState = FSMstate.CHASE;
        }
    }

    void Attack()
    {
        anim.SetInteger("animState", 2);
        agent.isStopped = true;

        if (attackTimer > attackDelay)
        {
            ph.TakeDamage(enemyDamage);
            attackTimer = 0;
        }

        if(playerDistance > attackDistance)
        {
            currentState = FSMstate.CHASE;
        }
        else
        {
            currentState = FSMstate.ATTACK;
        }
    }

    bool SightAlert()
    {
        if(playerDistance < sightDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Die()
    {
        anim.SetInteger("animState", 3);
        agent.isStopped = true;

        Destroy(gameObject, 5f);
    }

    public void setDead()
    {
        currentState = FSMstate.DEAD;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eyes.position, sightDistance);
    }

}
