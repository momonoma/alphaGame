using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    Transform player;
    float playerDistance;
    public void TriggerDialog()
    {
        
   
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Talking to you");
        FindObjectOfType<DialogManager>().StartDialogue(dialog);
    }

}
