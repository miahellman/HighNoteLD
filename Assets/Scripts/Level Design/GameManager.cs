using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gamePaused = false;
    public ModifyUI modifyUI;

    //singleton 
    public static GameManager instance;

    [HideInInspector] public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //find modifyUI component in canvas
        modifyUI = GameObject.Find("Canvas").GetComponent<ModifyUI>();
    }
    // Update is called once per frame
    void Update()
    {
        //pausing game function
        //pause game function for gameplay scene only
        if (Input.GetKeyUp(KeyCode.Escape) && gamePaused == false) { gamePaused = true; }
        else if (Input.GetKeyUp(KeyCode.Escape) && gamePaused == true) { gamePaused = false; }

        Time.timeScale = gamePaused ? 0 : 1;

    }

    //start game function
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    //game over function
    public void GameOver()
    {
        isGameOver = true;

        if (isGameOver)
        {
            SceneManager.LoadScene("End");
        }

    }

    public void EnterBathroom()
    {
        //enter the bathroom

        SceneManager.LoadScene("Bathroom");
    }

    public void ExitBathroom()
    {
        //enter the bathroom

        SceneManager.LoadScene("InsideScene");
    }

    //reset game function
    public void ResetGame()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
