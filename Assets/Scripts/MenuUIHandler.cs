using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Text bestScoreText;

    private void Awake()
    {
        DataManager.Instance.LoadDataFromFile();
    }
    private void Start()
    {
        bestScoreText.text = $"Best score: {DataManager.Instance.bestPlayerName} : {DataManager.Instance.HighScore}";
    }
    public void StartNew() 
    {
        DataManager.Instance.playerName = nameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void Exit() 
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
