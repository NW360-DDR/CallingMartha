using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactable
{
    private Animator doorAnimator;
    private bool doorOpen = false;
    private bool doorCooldown = false;

    public bool blocked = false;
    public GameObject blockingObject;
    public bool locked = false;
    public int keyNum = 0;

    private InventoryScript inventoryScript;
    void Interact()
    {
        //Do interact code here

        doorAnimator = GetComponentInParent<Animator>();
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

        //if locked is checked, check for correct key number
        if (locked)
        {
            if (inventoryScript.keys[keyNum] == true)
            {
                locked = false;
                inventoryScript.keys[keyNum] = false;
                logString = "Door Unlocked.";
                SendLog();
            }
            else
            {
                logString = "I need a key for this...";
                SendLog();
            }
        }

        //if blocked is checked, when object is gone, make blocked untrue
        if (blockingObject == null)
        {
            blocked = false;
        }
        else
        {
            logString = "There's something blocking this...";
            SendLog();
        }
            

        //actually open door
        if (!doorOpen && !locked && !doorCooldown && !blocked)
        {
            doorAnimator.SetTrigger("Open");
            doorOpen = true;
            doorCooldown = true;
            StartCoroutine(DoorCooldownTimer());
        }
        else if (!doorCooldown && !locked && !blocked)
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
