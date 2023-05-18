using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;
    public SceneFader sceneFader;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        sceneFader.FadeTo(nextLevel);
    }
    public void Menu()
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        sceneFader.FadeTo(menuSceneName);
    }
}
