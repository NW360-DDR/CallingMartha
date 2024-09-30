using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject[] enemies;
    void SpawnEnemies()
    {
        Debug.Log("It worked!");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(true);
        }

        Destroy(gameObject);
    }
}
