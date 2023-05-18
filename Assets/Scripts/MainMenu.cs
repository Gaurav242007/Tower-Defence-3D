using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;
    public void Play()
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        Application.Quit();
    }
}
