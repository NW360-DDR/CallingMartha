using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndRespawn : MonoBehaviour
{
    public int health = 3;

    public GameObject checkpoint;

    public bool alive = true;
    private bool respawned = false;
    private bool hurtCool = false;
    private InventoryScript InventoryScript;
    private GrabAndThrow grabScript;
    private bool healReset = false;
    private float healthHold = 0;

    private void Start()
    {
        InventoryScript = GetComponent<InventoryScript>();
        grabScript = GetComponent<GrabAndThrow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !respawned)
        {
            //kill player and run the respawn coroutine
            alive = false;
            GetComponent<CameraScript>().enabled = false;
            grabScript.enabled = false;
            GetComponent<AxeSlash>().enabled = false;
            GetComponent<NewPlayerMovement>().enabled = false;
            StartCoroutine(Respawn());
        }

        //heal if the player has a medkit and held down H for 1 second
        if (Input.GetKey(KeyCode.H) && InventoryScript.medKitCount > 0 && health < 3 && !healReset)
        {
            healthHold += Time.deltaTime;

            if (healthHold >= 1)
            {
                Debug.Log("Used medkit!");
                health = 3;
                InventoryScript.medKitCount -= 1;
            }
        }
        else
        {
            healthHold = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //player can only be hurt when hurtcooldown is over, also reset the healing timer if player is healing
        if (other.CompareTag("Hurtbox") && !hurtCool)
        {
            health -= 1;
            hurtCool = true;
            healReset = true;
            StartCoroutine(HitCooldown());
        }

        if (other.CompareTag("Spawn Trigger"))
        {
            other.GetComponent<EnemySpawnTrigger>().SendMessageUpwards("SpawnEnemies");
        }
    }

    IEnumerator Respawn()
    {
        //resets player location and health
        Debug.Log("Respawn?");
        respawned = true;
        transform.position = checkpoint.transform.position;
        health = 3;
        alive = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<CameraScript>().enabled = true;
        InventoryScript.enabled = true;
        GetComponent<AxeSlash>().enabled = true;
        GetComponent<NewPlayerMovement>().enabled = true;
        respawned = false;
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(1);
        healReset = false;
        hurtCool = false;
    }
}
