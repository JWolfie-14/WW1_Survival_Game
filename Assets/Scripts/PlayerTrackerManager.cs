using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTrackerManager : MonoBehaviour
{
    public GameStatusManager gameStatusManager;
    public CursorManager cursorManager;
    public float playerHealth;
    public float minPlayerHealth, maxPlayerHealth;
    public GameObject gameOverPanel;

    public TextMeshProUGUI salvagedSteelTextTMP;

    public TextMeshProUGUI destoyedTanksNumberTextTMP;
    public TextMeshProUGUI destoyedObstaclesNumberTextTMP;
    public Slider playerHealthbar;
    public TextMeshProUGUI playerHealthNumberTMP;

    int currentSalvagedSteel;
    int currentObstaclesDestroyed = 0;
    public int currentEnemyTanksDestroyed = 0;
    
    //both of these variables below effect the salvaged steel amounts
    public int smallHealthIncreasePurchase = 200;
    public int largeHealthIncreasePurchase = 400;
    //
    void Start() 
    {
        //startingPlayerHealth = playerhealth;
        UpdateHealthBarUI();
    }

    void Update()
    {
        Debug.Log("PlayerHealth: " + playerHealth);
    }
    public void AddSalvagedSteel(int addSalvagedSteel)
    {
        currentSalvagedSteel += addSalvagedSteel;
        UpdateSalvagedSteelTMP();
    }

    void UpdateSalvagedSteelTMP()
    {
        if (currentSalvagedSteel < 1000)
        {
            salvagedSteelTextTMP.text = currentSalvagedSteel.ToString("0");
        }
        else{
            salvagedSteelTextTMP.text = currentSalvagedSteel.ToString("0,000");
        }
    }

    void UpdateHealthBarUI()
    {
        playerHealthbar.value = playerHealth;
        playerHealthNumberTMP.text = playerHealth.ToString("0");
    }

    public void AddSmallHealthIncrease(int healthIncrease)
    {
        if (currentSalvagedSteel >= smallHealthIncreasePurchase && playerHealth < maxPlayerHealth)
        {
            playerHealth += healthIncrease;
            if(playerHealth > maxPlayerHealth)
            {
                playerHealth = maxPlayerHealth;
            }
            UpdateHealthBarUI();
            currentSalvagedSteel -= smallHealthIncreasePurchase;
            MakeSalvagedSteelNotGoUnder0();
            UpdateSalvagedSteelTMP();
        }
    }

    public void AddLargeHealthIncrease(int healthIncrease)
    {
        if (currentSalvagedSteel >= largeHealthIncreasePurchase && playerHealth < maxPlayerHealth)
        {
            playerHealth += healthIncrease;
            if(playerHealth > maxPlayerHealth)
            {
                playerHealth = maxPlayerHealth;
                
            }
            UpdateHealthBarUI();
            currentSalvagedSteel -= largeHealthIncreasePurchase;
            MakeSalvagedSteelNotGoUnder0();
            UpdateSalvagedSteelTMP();
        }
    }

    public void DecreasePlayerHealth(int playerDamaged)
    {
        playerHealth -= playerDamaged;
        UpdateHealthBarUI();
        if(playerHealth <= minPlayerHealth)
        {
            cursorManager.ActivateCursor();
            gameStatusManager.isPaused = true;
            gameOverPanel.SetActive(true);
            Debug.Log("Player destroyed");
        }
    }

    void MakeSalvagedSteelNotGoUnder0()
    {
        if (currentSalvagedSteel <= 0)
            {
                currentSalvagedSteel = 0;
            }
    }

    public void EnemyTanksDestoyed()
    {
        currentEnemyTanksDestroyed++;
        destoyedTanksNumberTextTMP.text = currentEnemyTanksDestroyed.ToString("0");
        Debug.Log("Tanks Destroyed: " + currentEnemyTanksDestroyed);
    }

    public void ObstacleDestoyed()
    {
        currentObstaclesDestroyed++;
        destoyedObstaclesNumberTextTMP.text = currentObstaclesDestroyed.ToString("0");
        Debug.Log("Obstacle Destroyed");
    }
}
