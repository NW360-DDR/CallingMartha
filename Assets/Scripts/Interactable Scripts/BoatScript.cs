using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : Interactable
{
    private HealthAndRespawn healthScript;
    public EclipseTimer eclipseTimer;
    public GameObject teleportPos;
    private GameObject player;
    void Interact()
    {
        player = GameObject.Find("Player (Remake)");
        healthScript = player.GetComponent<HealthAndRespawn>();

        healthScript.blackScreen.SetBool("FadeIn", true);
        eclipseTimer.enteredArena = true;
        StartCoroutine(MovePlayer());
    }

    IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(1);
        player.transform.position = teleportPos.transform.position;
        healthScript.blackScreen.SetBool("FadeIn", false);
    }
}
