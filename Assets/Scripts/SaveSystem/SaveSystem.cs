using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static string SaveDirectory = Application.persistentDataPath + "/saves/";
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(SaveDirectory))
            Directory.CreateDirectory(SaveDirectory);
        string path = SaveDirectory + saveName + ".save";

        FileStream file = new FileStream(path, FileMode.Create);

        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("Load file is not exist! path: " + path);
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            Debug.Log("End Loading");
            return save;
        }
        catch
        {
            Debug.Log("Failed to load file! path: " + path);
            file.Close();
            return null;
        }
    }
}
