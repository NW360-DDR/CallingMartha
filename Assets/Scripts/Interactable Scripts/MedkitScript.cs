using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitScript : Interactable
{
    private InventoryScript inventoryScript;
    void Interact()
    {
        //Do interact code here
        logString = "Picked up a Medkit.";
        SendLog();
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();
        inventoryScript.medKitCount += 1;

        Destroy(gameObject);
        Debug.Log("Medkit!");
    }
}
