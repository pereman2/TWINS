using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    private string defaultFilePath;

    private void Awake()
    {
        defaultFilePath = Application.persistentDataPath + "/data.dat";

        Load();
    }

    public void Load()
    {
        if (File.Exists(defaultFilePath))
            GameManager.UnlockedLevels = DeserializeFile(defaultFilePath).UnlockedLevels;
        else
            GameManager.UnlockedLevels = 1;
    }

    private GameData DeserializeFile(string path)
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        return binary.Deserialize(file) as GameData;
    }

    public void Save()
    {
        SerializeToFile(defaultFilePath);
    }

    private void SerializeToFile(string path)
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Create(defaultFilePath);

        GameData data = new GameData(GameManager.UnlockedLevels);

        binary.Serialize(file, data);
        file.Close();
    }
}


