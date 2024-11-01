using UnityEngine;

public class GeneratorInteractScript : Interactable
{
    private InventoryScript inventoryScript;
    private bool fixedCheck = false;
    public GameObject[] thingsToTurnOn;
    void Interact()
    {
        //Do interact code here

        Debug.Log("Gen Check!");
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

        if (inventoryScript.generatorItems < 3 && !fixedCheck)
        {
            Debug.Log("Sorry! Can't fix!");
            logString = "This generator needs work...";
            SendLog();
        }
        else
        {
            logString = "Generator repaired.";
            SendLog();
            fixedCheck = true;
            for (int i = 0; i < thingsToTurnOn.Length; i++)
            {
                thingsToTurnOn[i].SetActive(true);
            }
        }
    }
}
