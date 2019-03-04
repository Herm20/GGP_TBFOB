﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5.0f;

    private float countDown = 2.0f;

    public Text waveCountdownText;

    private int waveNumber = 0;


    private void Update()
    {
        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countDown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.75f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
