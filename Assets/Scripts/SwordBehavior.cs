using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    public Collider weapon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindObjectOfType<PlayerBehavior>().attacking)
        {
            weapon.enabled = true;
        }
        else
        {
            weapon.enabled = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Died");
            other.gameObject.SetActive(false);
        }
        */
        if (other.CompareTag("Boss"))
        {
            FindObjectOfType<BossBehavior>().TakeDamage(10);
        }
    }
}
