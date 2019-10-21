using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        string path = Application.persistentDataPath + "/saves" + saveName + ".save";

        FileStream file = new FileStream(path, FileMode.Create);

        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    public object Load(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("Load file is ont exist! path: " + path);
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
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
