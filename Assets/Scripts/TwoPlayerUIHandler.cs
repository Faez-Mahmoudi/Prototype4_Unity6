using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
# if UNITY_EDITOR
using UnityEditor;
# endif

public class TwoPlayerUIHandler : MonoBehaviour
{
    private int winOne;
    private int winTwo;

    [Header("Panel")]
    [SerializeField] private GameObject pausePanel;

    public bool paused;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI winText1;
    [SerializeField] private TextMeshProUGUI winText2;
    [SerializeField] private TextMeshProUGUI powerupText1;
    [SerializeField] private TextMeshProUGUI powerupText2;
    
    void Start()
    {
        winOne = 0;
        winTwo = 0;
        paused = false;
        Time.timeScale = 1;

        pausePanel.gameObject.SetActive(false);
        
        winText1.text = "Wins: " + winOne;
        winText2.text = "Wins: " + winTwo;
        powerupText1.text = "Powerup: None" ;
        powerupText2.text = "Powerup: None" ;
    }

    // Update is called once per frame
    void Update()
    {
        // ESC key pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePause();
    }

    public void AddWin(int value, int id)
    {
        if( id == 1 )
        {
            winOne += value;
            winText1.text = "Wins: " + winOne;
        }
        else if( id == 2 )
        {
            winTwo += value;
            winText2.text = "Wins: " + winTwo;
        } 
    }

    public void PrintPowerup(string powerupName, string id)
    {
        //var powerupText = 
        powerupText1.text = "Powerup: " + powerupName;
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Exit()
    {
        # if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        # else
        Application.Quit();
        # endif
    }
}