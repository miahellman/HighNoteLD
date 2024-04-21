using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XR;

public class ModifyUI : MonoBehaviour
{
    public GameManager gameManager;
    public CanExit canExit;

    [SerializeField] TextMeshProUGUI pauseText;
    [SerializeField] TextMeshProUGUI topText;



    // Update is called once per frame
    void Update()
    {
      
        //update text to show game is paused if paused is true
        pauseText.text = gameManager.gamePaused ? "paused" : "";
        topText.text = canExit.canExit ? "- 'space' to call it a night -" : "- take the edge off -";
    }
}
