using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Assign this to canvas with keybinds menu panel as child
public class MenuScript : MonoBehaviour
{
    private Transform menuPanel;
    private Event keyEvent;
    private Text buttonText;
    private KeyCode newKey;

    private bool waitingForKey;

    // Start is called before the first frame update
    private void Start()
    {
        menuPanel = transform.Find("Keybinds");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        for (var i = 0; i < 5; i++)
        {
            if (menuPanel.GetChild(i).name == "JumpKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.jump.ToString();
            }
            else if (menuPanel.GetChild(i).name == "LeftKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();
            }
            else if (menuPanel.GetChild(i).name == "RightKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();
            }
            else if (menuPanel.GetChild(i).name == "DownKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.down.ToString();
            }
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(true);
            FreezingPlayerHandler(true);
        } 
        else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(false);
            FreezingPlayerHandler(false);
        }
    }

    public static void FreezingPlayerHandler(bool freeze)
    {
        if (freeze)
        {
            PlayerController.freezePlayer();
            return;
        }
        
        PlayerController.unfreezePlayer();
    }

    private void OnGUI()
    {
        keyEvent = Event.current;

        if (!keyEvent.isKey || !waitingForKey) return;
        newKey = keyEvent.keyCode;
        waitingForKey = false;
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        yield return WaitForKey();

        switch (keyName)
        {
            case "jump":
                GameManager.GM.jump = newKey;
                buttonText.text = GameManager.GM.jump.ToString();
                PlayerPrefs.SetString("jumpKey", GameManager.GM.jump.ToString());
                break;
            case "left":
                GameManager.GM.left = newKey;
                buttonText.text = GameManager.GM.left.ToString();
                PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString());
                break;
            case "right":
                GameManager.GM.right = newKey;
                buttonText.text = GameManager.GM.right.ToString();
                PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString());
                break;
            case "down":
                GameManager.GM.down = newKey;
                buttonText.text = GameManager.GM.down.ToString();
                PlayerPrefs.SetString("downKey", GameManager.GM.down.ToString());
                break;
        }

        yield return null;
    }
}










