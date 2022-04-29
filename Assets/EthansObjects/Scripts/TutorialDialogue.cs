using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDialogue : MonoBehaviour
{
    public Text dialogueText;
    int sentIndex = 0;

    public string[] sentences =
    {
        "Hi, Player!  Welcome to Wired Together. (Click Arrow to Continue)",
        "Now that you're in the Cyber World, try walking around with WASD",
        "To get around quicker use LSHIFT to dash. Try it out now.",
        "That was easy enough, now lets try attacking. Hold E to attack.",
        "Here are some bots to practice against.",
        "Each level will have some enemies, once you destory all the enemies a door, like this, will allow you to move on",
        "Congrats, seems like you adjusted to the virtual world.  Try starting a new game",
        "Taking you to the title screen..."
    };

    public Button nextButton;
    public GameObject dummyEnemy;
    public static int dummyCount = 2;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = sentences[sentIndex];
        nextButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if(sentIndex == 4)
        {
            nextButton.gameObject.SetActive(false);

            if(dummyCount == 0)
            {
                nextButton.gameObject.SetActive(true);
            }
        }
    }

    public void nextText()
    {
        sentIndex++;
        if(sentIndex == 4)
        {
            SpawnBots();
        }
        if(sentIndex == 5)
        {
            door.SetActive(true);
        }

        if (sentIndex == sentences.Length)
        {
            SceneManager.LoadScene(0);
            return;
        }

        dialogueText.text = sentences[sentIndex];
    }

    void TaskOnClick()
    {
        nextText();
    }

    void SpawnBots()
    {
        Instantiate(dummyEnemy, new Vector3(-5, -22.7f, 15), new Quaternion(0,20,0,0));
        Instantiate(dummyEnemy, new Vector3(15, -22.7f, -10), new Quaternion(0, 10, 0, 0));
    }
}
