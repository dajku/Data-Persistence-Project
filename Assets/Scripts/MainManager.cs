using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText;
    public GameObject DimmingPanel; // Panel to grey out background when the game is over

    public GameObject WinText;
    public TextMeshProUGUI WinScoreText;
    public TextMeshProUGUI CongratulationText;

    public Paddle playerPaddle;

    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    

    
    // Start is called before the first frame update
    void Start()
    {
        WinText.SetActive(false);
        DimmingPanel.SetActive(false);
        // Displaying best score and high score at the top of the screen in main scene
        if (DataManager.Instance != null) 
        {
            BestScoreText.text = $"Best Score: {DataManager.Instance.bestPlayerName}: {DataManager.Instance.HighScore}";
        }
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        
        
    }

    private void Update()
    {
        if (CountAllBricks() == 0)
        {
            GameWon();
        }
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }

        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        DimmingPanel.SetActive(true);
        if (DataManager.Instance != null)
        {
            // if player score is greater than best score save the name of the player and its score to the json file
            if (m_Points > DataManager.Instance.HighScore)
            {
                DataManager.Instance.HighScore = m_Points;
                DataManager.Instance.bestPlayerName = DataManager.Instance.playerName;
                DataManager.Instance.SaveDataToFile();
            }
        }
    }

    public void GameWon() 
    {
        WinText.SetActive(true);
        Ball.velocity = Vector3.zero;
        if (playerPaddle != null) 
        {
            playerPaddle.enabled = false;
        }
        if (DataManager.Instance != null) 
        {
            CongratulationText.text = $"Congratulation! {DataManager.Instance.playerName}";
        }
        WinScoreText.text = $"Your score: {m_Points}";
        DimmingPanel.SetActive(true);
        WinText.SetActive(true);
    }
    public void BackToMenu() 
    {
        SceneManager.LoadScene(0);
    }

    int CountAllBricks()
    {
        Brick[] allBricks = FindObjectsOfType<Brick>();
        //Debug.Log($"Bricks count: {allBricks.Length}");
        return allBricks.Length;
    }
}
