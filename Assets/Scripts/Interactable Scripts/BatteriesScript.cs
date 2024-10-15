using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteriesScript : Interactable
{
    private InventoryScript inventoryScript;
    private FlashlightScript lightScript;
    void Interact()
    {
        //Do interact code here

        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();
        lightScript = GameObject.Find("Player (Remake)").GetComponent<FlashlightScript>();

        if (inventoryScript.flashLightBatteries <= 0)
        {
            lightScript.batteryLife = 100;
            lightScript.updatedBatteries = false;
            lightScript.flickered = false;
        }

        inventoryScript.flashLightBatteries += 1;

        Destroy(gameObject);
    }
}
