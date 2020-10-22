using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitsObstacles : MonoBehaviour
{
    private GameObject playerTrackerManager, enemyWavesManager;
    public int damage;
    public int armour;
    public bool isEnemy;
    void Start() 
    {
        enemyWavesManager = GameObject.Find("EnemyWavesManager");
        if (isEnemy)
        {
            armour = enemyWavesManager.GetComponent<EnemyWavesManager>().waveEnemyArmour;
            damage = enemyWavesManager.GetComponent<EnemyWavesManager>().waveEnemyDamageToPlayer;
        }
        
        playerTrackerManager = GameObject.Find("PlayerTrackerManager");
    }
    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<ObstaclesManager>().ObstacleTakenDamage(damage - armour);
            Destroy(gameObject);
        } 
        
        if (other.gameObject.CompareTag("Obstacles"))
        {
            other.gameObject.GetComponent<ObstaclesManager>().ObstacleTakenDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (isEnemy)
            {
                playerTrackerManager.GetComponent<PlayerTrackerManager>().DecreasePlayerHealth(damage);
                Destroy(gameObject);
            }
        }

        
    }
}
