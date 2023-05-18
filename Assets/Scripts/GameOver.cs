using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";


    public void Retry()
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        sceneFader.FadeTo(menuSceneName);
    }
}
