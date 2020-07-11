using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    
    public KeyCode jump { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode down { get; set; }

    private void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        } 
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        
        // Standard for saving to PlayerPrefs: <Keybind> + "Key"
        jump = (KeyCode) Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "W"));
        left = (KeyCode) Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode) Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        down = (KeyCode) Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey", "S"));
    }
}
