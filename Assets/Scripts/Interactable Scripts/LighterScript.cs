using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterScript : Interactable
{
    private InventoryScript inventoryScript;
    void Interact()
    {
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

        inventoryScript.lighters++;

        Destroy(gameObject);
    }
}
