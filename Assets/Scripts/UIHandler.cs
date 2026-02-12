using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
# if UNITY_EDITOR
using UnityEditor;
# endif

public class UIHandler : MonoBehaviour
{
    private int point;

    [Header("Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    public bool paused;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] public TextMeshProUGUI bestWaveText; // we will access it on SpawnManager.cs
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI bestPointText;
    [SerializeField] private TextMeshProUGUI powerupText;

    void Start()
    {
        point = 0;
        paused = false;

        MainManager.Instance.LoadScore();
        MainManager.Instance.isGameActive = true;
        bestPointText.SetText("Best Point: " + MainManager.Instance.bestPoint);

        gameOverPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        
        pointText.text = "Points: " + point;
        powerupText.text = "Powerup: None" ;
    
        //bestScoreText.text = PrintScore(MainManager.Instance.bestScore);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(MainManager.Instance.isGameActive)
        {
            if(Time.time >= nextTimeToAddScore && !paused)
            {
                ScoreUpdate();
                nextTimeToAddScore = Time.time + 0.03f;
            }
        }
        else    
            GameIsOver();            
        */
        // ESC key pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePause();
    }
    
    public void PrintWave(int value)
    {
        waveText.text = "Wave: " + value;
    }

    public void AddPoint(int value)
    {
        point += value;
        pointText.text = "Points: " + point;
    }

    public void PrintPowerup(string powerupName)
    {
        powerupText.text = "Powerup: " + powerupName;
    }
    
    public void ChangePause()
    {
        if (MainManager.Instance.isGameActive)
        {
            if(!paused)
            {
                paused = true;
                pausePanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void GameIsOver()
    {
        MainManager.Instance.SaveScore();
        gameOverPanel.gameObject.SetActive(true);
        MainManager.Instance.isGameActive = false;

        if (point > MainManager.Instance.bestPoint)
        {
            MainManager.Instance.bestPoint = point;
            bestPointText.SetText("Best Point: " + MainManager.Instance.bestPoint);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        MainManager.Instance.SaveScore();

        # if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        # else
        Application.Quit();
        # endif
    }
}