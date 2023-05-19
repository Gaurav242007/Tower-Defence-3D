using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gamaOverUI;
    public GameObject completeLevelUI;
    public static int MaxTurrets;
    public int levelMaxTurrets;
    public static int turretsCount = 0;
    public TMP_Text turretsLeft;

    void Awake()
    {

        MaxTurrets = levelMaxTurrets;
        turretsLeft.text = "0 Turret Left";
    }
    void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
            return;
        turretsLeft.text = turretsCount.ToString() + " Turrets Left";

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayLoseLevelSFX();
        GameIsOver = true;
        gamaOverUI.SetActive(true);
    }

    // this function will run when a level is completed
    public void WinLevel()
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayWinLevelSFX();
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
