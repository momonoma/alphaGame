using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    EnemyAI ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter: " + other.name);
        if(other.CompareTag("Sword"))
        {
            Debug.Log("ENEMY HIT BY SWORD");
            ai.setDead();
        }
    }
}
