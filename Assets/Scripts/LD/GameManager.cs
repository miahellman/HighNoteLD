using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gamePaused = false;
    public bool readyWASD;
    public bool readyMouse; 

    public float countDownWASD; 
    public float countDownMouse;

    public ModifyUI modifyUI;
    public CanExit canExit;

    //singleton 
    public static GameManager instance;
    public GameObject player; 

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

        //find player object
        player = GameObject.Find("Player");
   
    }
    // Start is called before the first frame update
    private void Start()
    {
        //find modifyUI component in canvas
        modifyUI = GameObject.Find("Canvas").GetComponent<ModifyUI>();
        //find canExit component in player
        canExit = GameObject.Find("Player").GetComponent<CanExit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countDownWASD > 0) { countDownWASD -= Time.deltaTime; } else { countDownWASD = 0; }
        if (countDownWASD <= 0) { readyWASD = true; }
        if (countDownMouse > 0) { countDownMouse -= Time.deltaTime; } else { countDownMouse = 0; }
        if (countDownMouse <= 0) { readyMouse = true; }

        Scene scene = SceneManager.GetActiveScene();
        //pausing game function
        //pause game function for gameplay scene only
        if (Input.GetKeyUp(KeyCode.Escape) && gamePaused == false) { gamePaused = true; }
        else if (Input.GetKeyUp(KeyCode.Escape) && gamePaused == true) { gamePaused = false; }

        Time.timeScale = gamePaused ? 0 : 1;

        if (canExit.canExit && Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("game over");
            GameOver();
        }

        //if 'x' pressed quick exit to end scene
        if (Input.GetKeyUp(KeyCode.X))
        {
            SceneManager.LoadScene("EndScene");
        }

        if (scene.name == "InsideScene")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; //disables the mouse cursor
        } 
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    //start game function
    public void StartGame()
    {
        SceneManager.LoadScene("InsideScene");
    }

    //game over function
    public void GameOver()
    {
        isGameOver = true;

        if (isGameOver)
        {
            SceneManager.LoadScene("EndScene");
        }

    }

    public void EnterBathroom()
    {
        //enter the bathroom

        SceneManager.LoadScene("Bathroom");
    }

    public void ExitBathroom()
    {
        //exit the bathroom

        SceneManager.LoadScene("InsideScene");
    }

    //reset game function
    public void ResetGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
