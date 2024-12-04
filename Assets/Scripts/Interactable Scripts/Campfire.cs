using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Campfire : Interactable
{
    NavMeshObstacle isLit;
    public GameObject objectsToTurnOn;
    private InventoryScript inventoryScript;
    void Interact()
    {
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();
        isLit = GetComponent<NavMeshObstacle>();

        if (isLit.enabled != true && inventoryScript.lighters > 0)
        {
            logString = "Used a Lighter";
            SendLog();
            isLit.enabled = true;
            objectsToTurnOn.SetActive(true);
            inventoryScript.lighters--;

            StartCoroutine(TurnOff());
        }else
        {
            logString = "I need a lighter for this campfire.";
            SendLog();

            Debug.Log("Fuck you!");
        }
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(60);

        isLit.enabled = false;
        objectsToTurnOn.SetActive(false);
    }
}
