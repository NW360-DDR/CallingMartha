using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class AxeScript : Interactable
{
    private InventoryScript inventoryScript;
    void Interact()
    {
        //Do interact code here

        if (GameObject.Find("Player (Remake)").GetComponentInChildren<GrabAndThrow>().canPickupAxe)
        {

            inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

            inventoryScript.axe = true;

            GameObject.Find("Player (Remake)").GetComponentInParent<AxeSlash>().rightHand.SetActive(false);
            GameObject.Find("Player (Remake)").GetComponentInParent<AxeSlash>().axeSprite.SetActive(true);

            Destroy(gameObject);
        }
    }
}
