using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KeyScript : Interactable
{
    private InventoryScript inventoryScript;
    public int keyNum = 0;
    void Interact()
    {
        //Do interact code here

        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

        inventoryScript.keys[keyNum] = true;
        Destroy(gameObject);
    }
}
