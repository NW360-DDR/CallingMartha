using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : Interactable
{
    private HealthAndRespawn healthScript;
    public GameObject teleportPos;
    void Interact()
    {
        healthScript = GameObject.Find("Player (Remake)").GetComponent<HealthAndRespawn>();

        healthScript.blackScreen.SetBool("FadeIn", true);
        StartCoroutine(MovePlayer());
    }

    IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Player (Remake)").transform.position = teleportPos.transform.position;
        healthScript.blackScreen.SetBool("FadeIn", false);
    }
}
