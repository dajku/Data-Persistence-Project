using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    public float accelerationRate = 0.01f;
    public float maxVelocity = 3.0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Instance already exists, destroying object");
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("DifficultyManager Awake");
    }

    private void Start()
    {
        LoadDataFromFile();
        Debug.Log("DifficultyManager Start");
    }


    public void EasyDifficulty()
    {
        maxVelocity = 2.0f;
        accelerationRate = 0.005f;
        SaveDataToFile();
        Debug.Log("Easy difficulty selected");
    }

    public void MediumDifficulty() 
    {
        maxVelocity = 3.0f;
        accelerationRate = 0.01f;
        SaveDataToFile();
        Debug.Log("Medium difficulty selected");
    }

    public void HardDifficulty()
    {
        maxVelocity = 5.0f;
        accelerationRate = 0.1f;
        SaveDataToFile();
        Debug.Log("Hard difficulty selected");
    }


    [System.Serializable]
    class SaveData
    {
        public float selectedAccelerationRate;
        public float selectedMaxVelocity;
    }
    public void SaveDataToFile()
    {
        
        SaveData data = new SaveData();
        Debug.Log(data);
        data.selectedAccelerationRate = accelerationRate;
        data.selectedMaxVelocity = maxVelocity;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/difficulty.json", json);
        Debug.Log("Data saved: " + json);
    }

    public void LoadDataFromFile()
    {
        string path = Application.persistentDataPath + "/difficulty.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            accelerationRate = data.selectedAccelerationRate;
            maxVelocity = data.selectedMaxVelocity;
            Debug.Log("Data loaded: " + json);
        }
        else 
        {
            Debug.Log("Save file not found");
        }

    }
}
