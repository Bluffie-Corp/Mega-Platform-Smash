using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindsManager : MonoBehaviour
{
   public KeyCode FetchKey()
   {
      var e = System.Enum.GetNames(typeof(KeyCode)).Length;
      for(var i = 0; i < e; i++)
      {
         if(Input.GetKey((KeyCode)i))
         {
            return (KeyCode)i;
         }
      }
             
      return KeyCode.None;
   }

   public void ChangeKeybind(string keybind, Text keybindText)
   {
      var keyPressed = FetchKey();
      var key = keyPressed.ToString();
      
      PlayerPrefs.SetString(keybind, key);
      PlayerPrefs.Save();

      keybindText.text = key;
   }

   public KeyCode GetKeybindInKeycode(string keybind)
   {
      var keyString = PlayerPrefs.GetString(keybind);
      var key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyString);
      return key;
   }
   
   public string GetKeybindInString(string keybind)
   { 
      return PlayerPrefs.GetString(keybind);
   }
}







