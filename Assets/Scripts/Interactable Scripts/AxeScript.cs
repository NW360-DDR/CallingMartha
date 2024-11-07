using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : Interactable
{
    private InventoryScript inventoryScript;
    void Interact()
    {
        //Do interact code here

        Debug.Log("Worked???");

        if (GameObject.Find("Player (Remake)").GetComponentInChildren<GrabAndThrow>().canPickupAxe)
        {

            inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

            inventoryScript.axe = true;

            Destroy(gameObject);
        }
    }
}
