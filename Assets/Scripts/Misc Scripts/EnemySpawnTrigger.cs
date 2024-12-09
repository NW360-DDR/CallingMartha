using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject[] enemies;
    public MusicTransition MusicTransition;
    // NOTE: This could also be done so that it just enables an empty parent of the enemie group and only need to enable one thing.
    void SpawnEnemies()
    {
        Debug.Log("It worked!");
        MusicTransition.SwitchToEncounter();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(true);
        }

        Destroy(gameObject);
    }
}
