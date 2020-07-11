using System;
using System.IO;
using CodeMonkey;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarForSavesScreen : MonoBehaviour
{
    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 25f;
    
    public Slider fillSlider;
    private Slider shieldSlider;
    private float damagedHealthShrinkTimer;
    private SaveManager _saveManager;

    private int healthBarNumber;
    
    private bool _saveDoesNotExist;
    private string folderName;

    public int currentSave;

    public PlayerStats stats;

    /*public Color darkRed = new Color(172, 0, 14, 255);
    public Color brightRed = new Color(255, 0, 14, 255);*/
    private Image _fillImage;

    private void Awake()
    {
        _fillImage = fillSlider
            .fillRect
            .gameObject
            .GetComponent<Image>();
        fillSlider = GetComponent<Slider>();
        shieldSlider = transform.Find("Shield Fill Slider").GetComponent<Slider>();
        _saveManager = FindObjectOfType<SaveManager>();
        var healthBarName = gameObject.name;
        /*switch (PlayerPrefs.GetString("PreferredSave"))
        {
            default:
                gameObject.SetActive(false);
                break;
            case "save1":
                if (healthBarName.Equals("Health Bar 2") || healthBarName.Equals("Health Bar 3")) break;
                gameObject.SetActive(true);
                healthBarNumber = 1;
                currentSave = 1;
                break;
            case "save2":
                if (healthBarName == "Health Bar 1" || healthBarName == "Health Bar 3") break;
                gameObject.SetActive(true);
                currentSave = 2;
                healthBarNumber = 2;
                break;
            case "save3":
                if (healthBarName == "Health Bar 1" || healthBarName == "Health Bar 2") break;
                gameObject.SetActive(true);
                currentSave = 3;
                healthBarNumber = 3;
                break;
        }*/

        if (healthBarName.Equals("Health Bar 1") /*|| healthBarName.Equals("Health Bar 3")*/)
        {
            healthBarNumber = 1;
        } else if (healthBarName.Equals("Health Bar 2") /*|| !healthBarName.Equals("Health Bar 3")*/)
        {
            healthBarNumber = 2;
        } else if (healthBarName.Equals("Health Bar 3") /*|| !healthBarName.Equals("Health Bar 1")*/)
        {
            healthBarNumber = 3;
        }
         
        switch (healthBarNumber)
        {
            case 1:
                folderName = "save1/";
                break;
            case 2:
                folderName = "save2/";
                break;
            case 3:
                folderName = "save3/";
                break;
        }
        
        Load();
    }

    private void Start()
    {
        ReloadUi();
        Debug.Log(folderName);
        fillSlider.maxValue = stats.maxHealth;
        shieldSlider.maxValue = stats.maxShield;
        shieldSlider.value = stats.shield;
        fillSlider.value = stats.health;
        fillSlider
                .fillRect
                .gameObject
                .GetComponent<Image>()
                .color = 
                stats.hasShieldActive ? new Color(127f / 255f, 0, 0, 255f / 255f) 
                    : new Color(1, 0, 0, 1);
    }

    public void DeleteSaveFile(int saveNumber)
    {
        var localFolderName = "";
        
        switch (saveNumber)
        {
            case 1:
                localFolderName = "save1/";
                break;
            case 2:
                localFolderName = "save2/";
                break;
            case 3:
                localFolderName = "save3/";
                break;
        }
        
        var filePath = Application.persistentDataPath + "/saves/" + localFolderName + "player.mps.save";

        if (!File.Exists(filePath))
        {
            Debug.LogErrorFormat("There is no MPS save file in {0}.", filePath);
        }
        else
        {
            File.Delete(filePath);
        }
        
        Load();
    }

    private void Load()
    {
        if (!File.Exists(Application.persistentDataPath + "/saves/" + folderName + "player.mps.save"))
        {
            _saveDoesNotExist = true;
        }
        else
        {
            stats = _saveManager.LoadPlayerStats(Application.persistentDataPath +
                                                 "/saves/" +
                                                 folderName +
                                                 "player.mps.save");
        }

        ReloadUi();
    }

    private void ReloadUi()
    {
        if (_saveDoesNotExist)
        {
            gameObject.SetActive(false);
            return;
        }
        fillSlider.maxValue = stats.maxHealth;
        shieldSlider.maxValue = stats.maxShield;
        shieldSlider.value = stats.shield;
        fillSlider.value = stats.health;
    }
}
