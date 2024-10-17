using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactable
{
    private Animator doorAnimator;
    private bool doorOpen = false;
    private bool doorCooldown = false;

    public bool locked = false;
    public int keyNum = 0;

    private InventoryScript inventoryScript;
    void Interact()
    {
        //Do interact code here

        doorAnimator = GetComponentInParent<Animator>();
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

        if (locked)
        {
            if (inventoryScript.keys[keyNum] == true)
            {
                locked = false;
                inventoryScript.keys[keyNum] = false;
            }
        }

        if (!doorOpen && !locked && !doorCooldown)
        {
            doorAnimator.SetTrigger("Open");
            doorOpen = true;
            doorCooldown = true;
            StartCoroutine(DoorCooldownTimer());
        }
        else if (!doorCooldown && !locked)
        {
            doorAnimator.SetTrigger("Close");
            doorOpen = false;
            doorCooldown = true;
            StartCoroutine(DoorCooldownTimer());
        }
    }

    IEnumerator DoorCooldownTimer()
    {
        yield return new WaitForSeconds(0.5f);
        doorCooldown = false;
    }
}
