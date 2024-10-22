using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : Interactable
{
    private InventoryScript inventoryScript;
    void Interact()
    {
        //Do interact code here
        logString = "Picked up Rock.";
        SendLog();
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();
        inventoryScript.bulletCount += 1;

        Destroy(gameObject);
    }
}
