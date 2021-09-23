using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenQuestMenu : MonoBehaviour
{
    [SerializeField] private GameObject questMenuUI;
    public static bool gameisPaused = false;

    public QuestItem neko, flower;

    private QuestDisplay questToDisplay;
    public GameManager gameManager;

    // private GameObject thePlayer;

    private void Awake()
    {
        // thePlayer = GameObject.Find("Player");
    }

    void Start()
    {
        Time.timeScale = 1f;

        questToDisplay = questMenuUI.GetComponentInChildren<QuestDisplay>();
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

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (gameManager != null)
            {
                swapQuestData(flower);
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (gameManager != null)
            {
                swapQuestData(neko);
            }
        }
    }

    public void questResumeGame()
    {
        questMenuUI.SetActive(false);

        Time.timeScale = 1f;
        gameisPaused = false;
    }

    public void questPauseGame()
    {
        questMenuUI.SetActive(true);

        Time.timeScale = 0f;
        gameisPaused = true;
    }

    private void FixedUpdate()
    {
        if (gameisPaused)
        {
            return;
        }
    }

    private void swapQuestData(QuestItem itemToSwap)
    {
        gameManager.currentQuest = itemToSwap;

        questToDisplay.questItem = itemToSwap;
        questToDisplay.title.text = itemToSwap.title;
        questToDisplay.difficulty.text = itemToSwap.difficulty;
        questToDisplay.description.text = itemToSwap.description;
        questToDisplay.questTaskDestination.sprite = itemToSwap.questTarget;
    }
}