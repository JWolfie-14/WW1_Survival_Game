using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    public GameObject parentObject, enemyWavesManager;
    public void DestroyTextAndStartWaves()
    {
        enemyWavesManager.GetComponent<EnemyWavesManager>().wavesTriggered = true;
        Destroy(parentObject);
    }
}
