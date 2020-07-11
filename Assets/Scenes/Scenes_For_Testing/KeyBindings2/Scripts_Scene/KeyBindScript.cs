using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScript : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    
    public Text jump, interact, pause, inventory, down, left, right, up, attack, block, menu, sprint; // 12 Keybinds
    private GameObject currentKey;
    
    public Color normal;
    public Color selected;
    
    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keys.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E")));
        keys.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "Escape")));
        keys.Add("Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "Z")));
        keys.Add("Attack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "Q")));
        keys.Add("Block", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Block", "X")));
        keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        keys.Add("Menu", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Menu", "F1")));

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
        interact.text = keys["Interact"].ToString();
        pause.text = keys["Pause"].ToString();
        inventory.text = keys["Inventory"].ToString();
        attack.text = keys["Attack"].ToString();
        block.text = keys["Block"].ToString();
        sprint.text = keys["Sprint"].ToString();
        menu.text = keys["Menu"].ToString();
    }

    // Update is called once per frame
    void Update()
    { 
        if(Input.GetKeyDown(keys["Jump"]))
        {
            Debug.Log("You Jumped");
        }
        if(Input.GetKeyDown(keys["Up"]))
        {
            Debug.Log("Upsies");
        }
    }

    private void OnGUI()
    {
        if (currentKey == null)
        {
            var e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }
    
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        
        PlayerPrefs.Save();
    }
}










