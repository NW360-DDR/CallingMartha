using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KeyScript : Interactable
{
    private InventoryScript inventoryScript;
    public int keyNum = 0;
    public string keyName;
    void Interact()
    {
        //Do interact code here
        logString = "Picked up "+ keyName;
        SendLog();
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

        inventoryScript.keys[keyNum] = true;
        Destroy(gameObject);
    }
}
