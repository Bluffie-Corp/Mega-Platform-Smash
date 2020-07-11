using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private PlayerController _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }
        
        if (!Directory.Exists(Application.persistentDataPath + "/saves/" + PlayerPrefs.GetString("PreferredSave", "save1")))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/" +
                                      PlayerPrefs.GetString("PreferredSave", "save1"));
        }

        var file = new FileStream(
            Application.persistentDataPath + "/saves/" + PlayerPrefs.GetString("PreferredSave", "save1") +
            "/player.mps.save", FileMode.OpenOrCreate);
        var formatter = new BinaryFormatter();
        
        formatter.Serialize(file, _player.stats);
        file.Close();
    }

    public void Load()
    {
        var file = new FileStream(Application.persistentDataPath + "/saves/" + PlayerPrefs.GetString("PreferredSave", "save1") + "/player.mps.save", FileMode.Open);
        var formatter = new BinaryFormatter();
        
        _player.stats = (PlayerStats) formatter.Deserialize(file);
        file.Close();
    }
    
    public PlayerStats LoadPlayerStats(string path)
    {
        var file = new FileStream(path, FileMode.Open);
        var formatter = new BinaryFormatter();
        
        var returnValue = (PlayerStats)formatter.Deserialize(file);
        file.Close();

        return returnValue;
    }
}
