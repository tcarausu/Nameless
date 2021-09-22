using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenQuestMenu : MonoBehaviour
{
    [SerializeField] private GameObject questMenuUI;
    public static bool gameisPaused = false;


    // private GameObject thePlayer;

    private PlayerBehaviourScript playerScript;

    private void Awake()
    {
        // thePlayer = GameObject.Find("Player");
        // playerScript = thePlayer.GetComponent<PlayerBehaviourScript>();
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameisPaused)
            {
                questResumeGame();
            }
            else
                questPauseGame();
        }

    }

    public void questResumeGame()
    {
        questMenuUI.SetActive(false);

        Time.timeScale = 1f;
        gameisPaused = false;

        playerScript.enabled = true;
    }


    public void questPauseGame()
    {
        questMenuUI.SetActive(true);

        Time.timeScale = 0f;
        gameisPaused = true;


        playerScript.enabled = false;
    }


    private void FixedUpdate()
    {
        if (gameisPaused)
        {
            return;
        }
    }
}