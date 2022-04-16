using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBehavior : MonoBehaviour
{
    Animator anim;
    Transform player;
    public GameObject text;
    public GameObject[] enemies;
    public bool dialogueEnd = false;

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
            transform.LookAt(player);
        }
        if (dialogueEnd == true)
        {

            gameObject.SetActive(false);
            foreach(GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }

        }
    }
}
