using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public TMP_Text waveCountdownText;
    public GameManager gameManager;
    private int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        Debug.Log(countdown);
        // first will take 2second (or default countdown) to spawn a wave
        // then afterwards will take 5 seconds (or timeBetweenWaves)
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        // reducing countdown to get to  0 and run SpawnWave()
        countdown -= Time.deltaTime;
        // adjusting/clamping the countdown value between 0 and infinity 
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        // formatting to time string
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    // use system.collections while using coroutine
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        // setting the number of enemies to wave count going to spawn enemies
        // instead of increasing enemy each time when spawning a single enemy
        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.count;
        for (int i = 0; i < wave.count; i++)
            SpawnEnemy(wave.enemy);
        yield return new WaitForSeconds(1f / wave.rate);

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
