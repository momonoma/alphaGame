using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int bulletDamage = 10;
    int currentHealth;
    NavMeshAgent agent;
    EnemyAI ai;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        ai = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Debug.Log("HIT! HEALTH: " + currentHealth);
            currentHealth = Mathf.Clamp(currentHealth - bulletDamage, 0, maxHealth);
        }

        if(currentHealth <= 0)
        {
            ai.setDead();
        }
    }
}
