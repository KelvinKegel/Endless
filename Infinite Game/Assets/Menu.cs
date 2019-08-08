using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPopUp;

    [SerializeField]
    private GameObject controlsPopUp;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void gameExit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void creditsButton()
    {
        creditsPopUp.SetActive(true);
    }

    public void controlsButton()
    {
        controlsPopUp.SetActive(true);
    }

    public void creditsButton(bool b)
    {
        creditsPopUp.SetActive(b);
    }

    public void controlsButton(bool b)
    {
        controlsPopUp.SetActive(b);
    }
}