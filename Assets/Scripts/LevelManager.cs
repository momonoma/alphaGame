using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static bool isGameOver = false;
    public static bool enemiesDead = false;
    public static bool bossDead = false;
    public static bool playerDead = false;
    public GameObject finalExit;
    public AudioClip win;
    public AudioClip lose;
    public Text statusText;
    public GameObject status;
    public bool gameWon = false;

    private void Awake()
    {
        isGameOver = false;
        enemiesDead = false;
        playerDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemies == 0)
            {
                enemiesDead = true;
            }

            if (bossDead)
            {
                finalExit.SetActive(true);
            }

            if (playerDead)
            {
                LevelLost();
            }


        }
    }



    public void LevelLost()
    {
        setGameOverStatus("Rebooting...", lose);
        Invoke("LoadCurrentLevel", 5);

    }

    public void LevelWon()
    {
        setGameOverStatus("Game Clear!", win);
    }

    void setGameOverStatus(string gameTextMessage, AudioClip sfx)
    {
        isGameOver = true;
        AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
        status.SetActive(true);
        statusText.text = gameTextMessage;
        statusText.enabled = true;
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevelDelay(int delay)
    {
        Invoke("LoadNextLevel", delay);
    }

}
