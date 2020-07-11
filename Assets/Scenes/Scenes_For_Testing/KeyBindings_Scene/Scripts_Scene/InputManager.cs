using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public KeyBindings keybindings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public bool KeyDown(string key)
    {
        return Input.GetKeyDown(keybindings.CheckKey(key));
    }

    public bool GetKey(string key)
    {
        return Input.GetKey(keybindings.CheckKey(key));
    }
}








