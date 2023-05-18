using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;
    // Array that takes Buttons (data types) Only
    public Button[] levelButtons;

    void Start()
    {
        // if the game has never been played before
        // then the default state for levelReached equal --> 1
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            // not want the button to be clickable
            // and make the button disabled

            // if the level is reached
            // then disable the button of that particular level index
            // say level reached ==> 2
            // then then i = 0; 0 + 1 > 2 -> false; ==> button will be enable
            // i=1 1+1 > 2 -> false; ==> button will be enable
            // i = 2, 2 + 1 > 2 -> true; then that button will be disabled
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        sceneFader.FadeTo(levelName);
    }

}
