using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager2 : MonoBehaviour
{
    public GameObject textBox;
    public Text nameText;
    public Text dialogText;
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialog dialogue)
    {
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.dialog)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }
    void EndDialogue()
    {
        Debug.Log("Ending convo");
        textBox.SetActive(false);
        FindObjectOfType<FinalNPCBehavior>().dialogueEnd = true;

    }
}
