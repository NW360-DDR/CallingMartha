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
            isLit.enabled = true;
            objectsToTurnOn.SetActive(true);
            inventoryScript.lighters--;

            StartCoroutine(TurnOff());
        }else
        {
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
