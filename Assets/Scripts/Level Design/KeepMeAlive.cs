using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMeAlive : MonoBehaviour
{
    public static KeepMeAlive Instance { get; private set; }
    private void Awake() //later add stuff that doesn't do this if the player blacks out
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
