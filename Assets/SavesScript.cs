using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavesScript : MonoBehaviour
{
    public PlayerStats stats;
    
    [HideInInspector] 
    public int currentSave;

    public Button saveOneButton;
    public Button saveTwoButton;
    public Button saveThreeButton;

    public LevelLoader LevelLoader;

    public TextMeshProUGUI selectedText1;
    public TextMeshProUGUI selectedText2;
    public TextMeshProUGUI selectedText3;

    private void Awake()
    {
        selectedText1.gameObject.SetActive(false);
        selectedText2.gameObject.SetActive(false);
        selectedText3.gameObject.SetActive(false);
        var selectedSave = PlayerPrefs.GetString("PreferredSave");
        if (selectedSave == null) return;
        switch (selectedSave)
        {
            default:
                selectedText1.gameObject.SetActive(false);
                selectedText2.gameObject.SetActive(false);
                selectedText3.gameObject.SetActive(false);
                break;
            case "save1":
                selectedText1.gameObject.SetActive(true);
                break;
            case "save2":
                selectedText2.gameObject.SetActive(true);
                break;
            case "save3":
                selectedText3.gameObject.SetActive(true);
                break;
        }
    }

    /*private void OnGUI()
    {
        saveOneButton.onClick.AddListener(SetSaveOneAsDefault);
        saveTwoButton.onClick.AddListener(SetSaveTwoAsDefault);
        saveThreeButton.onClick.AddListener(SetSaveThreeAsDefault);
    }*/

    public void SetSaveOneAsDefault()
    {
        currentSave = 0;
        PlayerPrefs.SetString("PreferredSave", "save1");
        Directory.CreateDirectory(Application.persistentDataPath + "/saves/" + PlayerPrefs.GetString("PreferedSave", "save1"));
        LevelLoader.LoadLevel(3);
    }

    public void SetSaveTwoAsDefault()
    {
        currentSave = 1;
        PlayerPrefs.SetString("PreferredSave", "save2");
        Directory.CreateDirectory(Application.persistentDataPath + "/saves/" +
                                  PlayerPrefs.GetString("PreferredSave", "save1"));
        LevelLoader.LoadLevel(3);
    }
    
    public void SetSaveThreeAsDefault()
    {
        currentSave = 2;
        PlayerPrefs.SetString("PreferredSave", "save3");
        Directory.CreateDirectory(Application.persistentDataPath + "/saves/" +
                                  PlayerPrefs.GetString("PreferredSave", "save1"));
        LevelLoader.LoadLevel(3);
    }
}
