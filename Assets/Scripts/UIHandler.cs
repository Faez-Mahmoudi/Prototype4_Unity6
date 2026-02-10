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
    [SerializeField] private TextMeshProUGUI bestWaveText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI bestPointText;
    [SerializeField] private TextMeshProUGUI powerupText;

    void Start()
    {
        //MainManager.Instance.LoadScore();
        point = 0;
        paused = false;
        gameOverPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        
        pointText.text = "Points: " + point;
        powerupText.text = "Powerup: None" ;
        //scoreText.text = PrintScore(score);
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
        if (true)//GameManager.Instance.isGameActive)
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
        gameOverPanel.gameObject.SetActive(true);
        /*
        if (score > MainManager.Instance.bestScore)
        {
            MainManager.Instance.bestScore = score;
            bestScoreText.SetText(PrintScore(MainManager.Instance.bestScore));
        }

        // Continue butten activated
        if (MainManager.Instance.dollars >= 10 && !MainManager.Instance.isContinueued)
            continueButton.interactable = true;
        else
            continueButton.interactable = false;
        */
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameManager.Instance.isGameActive = true;
    }

    public void Exit()
    {
        //MainManager.Instance.SaveScore();

        # if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        # else
        Application.Quit();
        # endif
    }
}