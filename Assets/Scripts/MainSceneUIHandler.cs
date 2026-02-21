using UnityEngine;
using UnityEngine.SceneManagement;
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
        OnButtonPress(false, false);
    }

    public void OnePlayer()
    {
        sceneName = "OnePlayerScene";
        OnButtonPress(true, false);
    }

    public void TwoPlayer()
    {
        sceneName = "TwoPlayerScene";
        OnButtonPress(false, true);
    }

    private void OnButtonPress(bool setP1, bool setP2)
    {
        player.gameObject.SetActive(setP1);
        playerOne.gameObject.SetActive(setP2);
        playerTwo.gameObject.SetActive(setP2);
    }

    public void PlayGame()
    {
        if(sceneName != "")
            SceneManager.LoadScene(sceneName);
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