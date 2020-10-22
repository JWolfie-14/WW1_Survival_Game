using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWavesManager : MonoBehaviour
{
    public GameStatusManager gameStatusManager;
    public Transform[] enemySpawnPoints;
    public List <int> enemyDamageOnEachWave, enemyArmourOnEachWave;
    public int waveEnemyArmour;
    public int waveEnemyDamageToPlayer;
    public GameObject enemyTankPrefab;
    public GameObject playerTrackerManager;
    public int enemiesPerWave = 3;
    public GameObject newWavePanel, initialWavePanel;
    public int currentWave = 0;
    public int maxNumberOfWaves;
    bool newWaveInitiated = true;
    bool isCountdownActive = true;
    bool spawnAI = false;
    public string newWaveTextString;

    public float timeLimit;

    public bool wavesTriggered = false;
    float currentTimeLeft, startingTime;
    public TextMeshProUGUI timerTextNextWaveTMP, timerTextInitialWaveTMP, waveTextTMP, waveTextMainUITMP;

    public void StartGame()
    {
        Debug.Log("GameStarted");
        

    }
    void Update()
    {
        if (GameObject.FindWithTag("Enemy") == null && gameStatusManager.hasWave1Played)
        {
            NewWaveTimerAndUIInformation(newWavePanel, timerTextNextWaveTMP);
            Debug.Log("empty scene of enemies");
        }  
        if (GameObject.FindWithTag("Enemy") == null && wavesTriggered == true && !gameStatusManager.hasWave1Played)
        {
            NewWaveTimerAndUIInformation(initialWavePanel, timerTextInitialWaveTMP);
        }
    }

    void NewWaveTimerAndUIInformation(GameObject wavePanel, TextMeshProUGUI timerTextTMP)
    {
        if (isCountdownActive && !gameStatusManager.isPaused)
        {
            wavePanel.SetActive(true);
            if (newWaveInitiated == true)
            {
                currentTimeLeft = timeLimit;
                //startingTime = Time.time;
                waveTextTMP.text = "Wave " + currentWave + " Clear";
                currentWave++;
                waveTextMainUITMP.text = "Wave " + currentWave;
                newWaveInitiated = false;
            }
            //currentTimeLeft = timeLimit - (Time.time - startingTime);
            currentTimeLeft -= 1 * Time.deltaTime;
            timerTextTMP.text = currentTimeLeft.ToString("0");
            Debug.Log(currentTimeLeft);
            if(currentTimeLeft<=0 && !spawnAI)
            {
                isCountdownActive = false;
                spawnAI = true;
                wavePanel.SetActive(false);
                SpawnEnemies();
            }
        }
    }

    public void InitialWave()
    {
        
    }

    void SpawnEnemies()
    {
        //newWavePanel.SetActive(false);
        List<Transform> freeSpawnPoints = new List<Transform>(enemySpawnPoints);
        for (int i = 0; i < enemiesPerWave; i++)
        {
            if (freeSpawnPoints.Count <=0)
            {
                return; // Not enough spawn points
            }
            int index = Random.Range(0, freeSpawnPoints.Count);
            Transform chosenSpawnPoint = freeSpawnPoints[index];
            freeSpawnPoints.RemoveAt(index); // remove the spawnpoint from our temporary list
            Instantiate(enemyTankPrefab, chosenSpawnPoint.position, chosenSpawnPoint.rotation);
        }   
        isCountdownActive = true;
        newWaveInitiated = true;
        spawnAI = false;
        gameStatusManager.hasWave1Played = true;
        UpdateEnemyWaveStatistics();
    }
    void UpdateEnemyWaveStatistics()
    {
        if (currentWave < maxNumberOfWaves)
        {
            waveEnemyArmour = enemyArmourOnEachWave[currentWave];
            waveEnemyDamageToPlayer = enemyDamageOnEachWave[currentWave];
        }
        else{
            waveEnemyArmour = enemyArmourOnEachWave[maxNumberOfWaves];
            waveEnemyDamageToPlayer = enemyDamageOnEachWave[maxNumberOfWaves];
        }
    }
}
