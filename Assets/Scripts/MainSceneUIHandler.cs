using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
# if UNITY_EDITOR
using UnityEditor;
# endif

public class MainSceneUIHandler : MonoBehaviour
{
    private string sceneName;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;


    private void Start()
    {
        sceneName = "";
        Time.timeScale = 1;
        player.gameObject.SetActive(false);
        playerOne.gameObject.SetActive(false);
        playerTwo.gameObject.SetActive(false);
    }

    public void OnePlayer()
    {
        sceneName = "OnePlayerScene";
        player.gameObject.SetActive(true);
        playerOne.gameObject.SetActive(false);
        playerTwo.gameObject.SetActive(false);
    }

    public void TwoPlayer()
    {
        sceneName = "TwoPlayerScene";
        player.gameObject.SetActive(false);
        playerOne.gameObject.SetActive(true);
        playerTwo.gameObject.SetActive(true);
    }

    public void PlayGame()
    {
        if(sceneName != "")
            SceneManager.LoadScene(sceneName);
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