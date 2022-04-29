using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalNPCBehavior : MonoBehaviour
{
    Animator anim;
    Transform player;
    public GameObject text;
    public bool dialogueEnd = false;
    public GameObject cont;
    float distanceToPlayer;
    public GameObject[] wanders;
    int currentDestIndex = 0;

    public int numObjects = 4;
    public GameObject prefab;
    public enum FSMstate
    {
        IDLE,
        DISAPPEAR,
        ATTACK
    }
    public FSMstate currentState;
    float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        dialogueEnd = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance < 10)
        {
            cont.SetActive(true);
            transform.LookAt(player);
        }
        if (dialogueEnd == true)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            switch (currentState)
            {
                case FSMstate.IDLE:
                    UpdateIdleState();
                    break;
                case FSMstate.DISAPPEAR:
                    UpdateTravelState();
                    break;
                case FSMstate.ATTACK:
                    UpdateAttackState();
                    break;
            }
        }
    }

    void UpdateIdleState()
    {
        anim.SetInteger("NPCanim", 0);
        if (distanceToPlayer <= 40)
        {
            currentState = FSMstate.ATTACK;
        }
    }

    void UpdateTravelState()
    {
        anim.SetInteger("NPCanim", 2);
        transform.position = wanders[currentDestIndex].transform.position;
        currentDestIndex = (currentDestIndex + 1) % wanders.Length;
        currentState = FSMstate.IDLE;
    }

    void UpdateAttackState()
    {
        anim.SetInteger("NPCanim", 1);
        FaceTarget(player.transform.position);

        StartCoroutine("AttackOnce");

        StartCoroutine("ExecuteAfterTime", 5);

    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void EnemyAttack()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = RandomCircle(center, 5.0f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            var bullet = Instantiate(prefab, pos, rot);
            
        }
    }
    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        currentState = FSMstate.DISAPPEAR;
    }

    IEnumerator AttackOnce()
    {
        EnemyAttack();
        yield return new WaitForSeconds(5);
    }
}
