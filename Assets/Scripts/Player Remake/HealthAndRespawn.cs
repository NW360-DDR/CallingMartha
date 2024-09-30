using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndRespawn : MonoBehaviour
{
    public float health = 100;

    public GameObject checkpoint;

    public bool alive = true;
    private bool respawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !respawned)
        {
            alive = false;
            GetComponent<CameraScript>().enabled = false;
            GetComponentInChildren<GrabAndThrow>().enabled = false;
            GetComponent<AxeSlash>().enabled = false;
            GetComponent<NewPlayerMovement>().enabled = false;
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Hurtbox"))
        {
            health -= 15;
        }

        if (other.tag == ("Spawn Trigger"))
        {
            other.GetComponent<EnemySpawnTrigger>().SendMessageUpwards("SpawnEnemies");
        }
    }

    IEnumerator Respawn()
    {
        Debug.Log("Respawn?");
        respawned = true;
        transform.position = checkpoint.transform.position;
        health = 100;
        alive = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<CameraScript>().enabled = true;
        GetComponentInChildren<GrabAndThrow>().enabled = true;
        GetComponent<AxeSlash>().enabled = true;
        GetComponent<NewPlayerMovement>().enabled = true;
        respawned = false;
    }
}
