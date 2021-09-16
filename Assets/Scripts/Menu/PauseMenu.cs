using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI, optionsMenuUI, deadMenuUI, wonMenuUI;
    public static bool gameisPaused = false;


    private GameObject thePlayer;

    private PlayerBehaviourScript playerScript;

    private bool isMeleeScriptActive = true;

    private void Awake()
    {

        thePlayer = GameObject.Find("Player");

        playerScript = thePlayer.GetComponent<PlayerBehaviourScript>();
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
                ResumeGame();
            }
            else
                PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            WonGame();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {

            DeadGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        deadMenuUI.SetActive(false);
        wonMenuUI.SetActive(false);

        Time.timeScale = 1f;
        gameisPaused = false;

        playerScript.enabled = true;

    }

    public void DeadGame()
    {
        deadMenuUI.SetActive(true);

        Time.timeScale = 0f;
        gameisPaused = true;


        playerScript.enabled = false;

    }


    public void WonGame()
    {
        wonMenuUI.SetActive(true);

        Time.timeScale = 0f;
        gameisPaused = true;


        playerScript.enabled = false;

    }


    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        gameisPaused = true;


        playerScript.enabled = false;

       
    }


    public void QuitGame()
    {
        Application.Quit();
    }



    public void OpenMenu()
    {
        isMeleeScriptActive = true;

        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        if (gameisPaused)
        {
            return;
        }
    }
}
