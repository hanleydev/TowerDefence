using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public float enemyDelay = 0.5f;
    private float countdown = 2f;
    private int waveIndex = 0;

    public Text waveCountdownText;

    void Update()
    {
        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    //TODO: Watch WaveSpawner MiniSeries - Brackeys
    IEnumerator SpawnWave() {
        Debug.Log("Wave Incoming");
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(enemyDelay);
        }
    }

    void SpawnEnemy() {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
