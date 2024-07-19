using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Text bestScoreText;

    private void Start()
    {
        if (DataManager.Instance.bestPlayerName != null && DataManager.Instance.HighScore != 0) 
        {
            bestScoreText.gameObject.SetActive(true);
            bestScoreText.text = $"Best score: {DataManager.Instance.bestPlayerName}: {DataManager.Instance.HighScore}";
        }
        else 
        {
            bestScoreText.gameObject.SetActive(false);
        }
    }
    public void StartNew() 
    {
        // Loading player name from the input field and switching scene to main game
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
