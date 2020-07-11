using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{
    public static bool Save(string saveName, object saveData)
    {
        var formatter = NewBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        var path = Application.persistentDataPath + "/saves/" + saveName + ".mpss";

        var file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }
        
        var formatter = NewBinaryFormatter();
        var file = File.Open(path, FileMode.Open);

        try
        {
            var save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Failed to load file at path {0}", path);
            Debug.LogErrorFormat("And this error surged: {0}", e);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter NewBinaryFormatter()
    {
        var formatter = new BinaryFormatter();

        return formatter;
    }
}




























