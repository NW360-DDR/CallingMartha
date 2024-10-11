using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private HealthAndRespawn healthScript;
    void Interact()
    {
        healthScript = GameObject.Find("Player (Remake)").GetComponent<HealthAndRespawn>();

        Debug.Log("Updated Checkpoint!");
        healthScript.checkpoint = this.gameObject;
    }
}
