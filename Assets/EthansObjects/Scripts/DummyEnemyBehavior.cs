using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemyBehavior : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sword"))
        {
            anim.SetInteger("animState", 3);
            Destroy(gameObject, 3f);
        }
    }

    private void OnDestroy()
    {
        TutorialDialogue.dummyCount--;
    }
}
