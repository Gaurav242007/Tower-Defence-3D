using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource selectTurret;
    public AudioSource upgradeTurret;
    public AudioSource instantiateTurret;
    public AudioSource enemyDestroyEffect;
    public AudioSource sellSFX;
    public AudioSource winLevelSFX;
    public AudioSource loseLevelSFX;
    public AudioSource buttonClickSFX;
    public void PlaySelectTurretSFX()
    {
        selectTurret.Play();
    }
    public void PlayUpgradeTurretSFX()
    {
        upgradeTurret.Play();
    }
    public void PlayInstantiateTurretSFX()
    {
        instantiateTurret.Play();
    }
    public void PlaySellSFX()
    {
        sellSFX.Play();
    }
    public void PlayEnemyDeathSFX()
    {
        enemyDestroyEffect.Play();
    }

    public void PlayWinLevelSFX()
    {
        winLevelSFX.Play();
    }
    public void PlayLoseLevelSFX()
    {
        loseLevelSFX.Play();
    }

    public void PlayButtonClickSFX()
    {
        buttonClickSFX.Play();
    }

}
