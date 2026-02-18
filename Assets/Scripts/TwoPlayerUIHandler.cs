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
    [SerializeField] private TextMeshProUGUI keyReminderText1;
    [SerializeField] private TextMeshProUGUI keyReminderText2;
    
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
        keyReminderText1.text = "";
        keyReminderText2.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // ESC key pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePause();
    }

    public void AddWin(int value, string id)
    {
        if( id == "2" )
        {
            winOne += value;
            winText1.text = "Wins: " + winOne;
        }
        else if( id == "1" )
        {
            winTwo += value;
            winText2.text = "Wins: " + winTwo;
        } 
    }

    public void PrintPowerup(PowerUpType powerup, string id)
    {
        if( id == "1" )
        {
            powerupText1.text = "Powerup: " + powerup.ToString();
            if(powerup == PowerUpType.Smash)
                keyReminderText1.text = "press K to smash";
            else if(powerup == PowerUpType.Rockets)
                keyReminderText1.text = "press L to Fire";
            else
                keyReminderText1.text = "";
        }
        else if( id == "2" )
        {
            powerupText2.text = "Powerup: " + powerup.ToString();
            if(powerup == PowerUpType.Smash)
                keyReminderText2.text = "press F to smash";
            else if(powerup == PowerUpType.Rockets)
                keyReminderText2.text = "press G to Fire";
            else
                keyReminderText2.text = "";
        }
    }
    
    public void ChangePause()
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