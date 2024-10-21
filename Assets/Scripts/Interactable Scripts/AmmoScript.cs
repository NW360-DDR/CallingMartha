using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : Interactable
{
    private InventoryScript inventoryScript;

    public int bulletCount = 2;
    void Interact()
    {
        //Do interact code here

        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();
        inventoryScript.bulletCount += bulletCount;

        Destroy(gameObject);
    }
}
