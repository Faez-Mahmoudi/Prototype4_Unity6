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

    private void Start()
    {
        sceneName = "";
        Time.timeScale = 1;
    }

    public void OnePlayer()
    {
        sceneName = "OnePlayerScene";
    }

    public void TwoPlayer()
    {
        sceneName = "TwoPlayerScene";
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