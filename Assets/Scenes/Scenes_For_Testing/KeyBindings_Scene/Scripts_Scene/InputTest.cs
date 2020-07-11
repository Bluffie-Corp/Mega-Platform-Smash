using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTest : MonoBehaviour
{
    public Text testText;
    public bool isFiveActive = false;
    
    private void Update()
    {
        if (InputManager.instance.KeyDown("Interact"))
        {
            testText.text = !isFiveActive ? "Miko - ME_K.O" : "Five - HI_5";

            isFiveActive = !isFiveActive;
        }
    }
}
