using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public int obstacleHealth = 100;

    public int minQuantityOfSalvagedSteel;
    public int maxQuantityOfSalvagedSteel;
    GameObject playerTrackerManager;
    public GameObject tankCarcass;
    private Transform parentEnemyTank;

    public bool isTank;

    int salvagedSteelLooted;

    void Start() 
    {
        playerTrackerManager = GameObject.Find("PlayerTrackerManager");
        parentEnemyTank = transform.parent;
        //putting this here \/ then called the addsalvagedsteel function with salvagedsteellooted passed into the function when just before destroyed 
        salvagedSteelLooted = Random.Range(minQuantityOfSalvagedSteel, maxQuantityOfSalvagedSteel);
    }
    public void ObstacleTakenDamage(int damageTaken)
    {
        obstacleHealth -= damageTaken;
        
        if(obstacleHealth <= 0)
        {
            playerTrackerManager.GetComponent<PlayerTrackerManager>().AddSalvagedSteel(salvagedSteelLooted);
            if (gameObject.CompareTag("Enemy"))
            {
                playerTrackerManager.GetComponent<PlayerTrackerManager>().EnemyTanksDestoyed();
            }
            if (gameObject.CompareTag("Obstacles"))
            {
                playerTrackerManager.GetComponent<PlayerTrackerManager>().ObstacleDestoyed();
            }
            Destroy(gameObject);
            if (isTank == true)
            {
                Quaternion carcassRotation = Quaternion.Euler(0, parentEnemyTank.rotation.eulerAngles.y, 0);
                Destroy(parentEnemyTank.transform.gameObject);
                Instantiate(tankCarcass, parentEnemyTank.position, carcassRotation);
            }
        }
    }
}