using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName;
    public string bestPlayerName;
    public int HighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (File.Exists(Application.persistentDataPath + "savefile.json"))
        {
            LoadDataFromFile();    
        }
    }

    [System.Serializable]

    class SaveData
    {
        public int BestScore;
        public string BestPlayerName;
    }

    public void SaveDataToFile() 
    {
        SaveData data = new SaveData();
        data.BestScore = HighScore;
        data.BestPlayerName = bestPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadDataFromFile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) 
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.BestPlayerName;
            HighScore = data.BestScore;
        }
    }

}
