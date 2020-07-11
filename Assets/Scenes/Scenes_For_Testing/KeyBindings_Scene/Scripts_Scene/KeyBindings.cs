using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "KeyBindings", menuName = "KeyBindings")]
public class KeyBindings : ScriptableObject
{
    public KeyCode jump, interact, pause, inventory, down, left, right, up, attack, block, menu;

    public KeyCode CheckKey(string key)
    {
        switch (key)
        {
          case "Jump":
              return jump;
          
          case "Interact":
              return interact;
          
          case "Pause":
              return pause;
          
          case "Inventory":
              return inventory;
          
          case "Down":
              return down;
          
          case "Left":
              return left;
          
          case "Right":
              return right;
          
          case "Up":
              return up;
          
          case "Attack":
              return attack;
          
          case "Block":
              return block;
          
          case "Menu":
              return menu;
          
          default:
              return KeyCode.None;
        }
    }
}
