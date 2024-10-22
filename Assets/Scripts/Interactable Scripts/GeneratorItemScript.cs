using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorItemScript : Interactable
{
    private InventoryScript inventoryScript;
    void Interact()
    {
        //Do interact code here
        logString = "This looks like it'll help to fix a generator...";
        SendLog();
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

        inventoryScript.generatorItems += 1;
        Destroy(gameObject);
    }
}
