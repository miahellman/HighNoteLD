using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XR;

public class ModifyUI : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] TextMeshProUGUI pauseText;



    // Update is called once per frame
    void Update()
    {
      
        //update text to show game is paused if paused is true
        pauseText.text = gameManager.gamePaused ? "paused" : "";
    }
}
