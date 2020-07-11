using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int gold = 0;
    // private int saveGold = SaveData.SD.profile.gold;
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        SaveData.Current = (SaveData) SerializationManager.Load(Application.persistentDataPath + "/saves/save.mpss");
    }

    private void Start()
    {
        gold = SaveData.Current.profile.gold;
    }

    public void AddGold(int goldAmount)
    {
        gold += goldAmount;
        goldText.text = $"Gold: {gold}";
        SaveData.Current.profile.gold += goldAmount;
    }
    
    public void SustractGold(int goldAmount)
    {
        gold -= goldAmount;
        goldText.text = $"Gold: {gold}";
        SaveData.Current.profile.gold -= goldAmount;
    }

    public void UISave()
    {
        var isOK = SerializationManager.Save("Save #1", gold);
        if (!isOK)
        {
            Debug.LogError("Sorry there was an error!");
        }
    }

    public void OnLoad()
    {
        var saveGold = SaveData.Current.profile.gold;
        goldText.text = $"Gold: {saveGold}";
    }
}
