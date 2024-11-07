using UnityEngine;

public class GeneratorInteractScript : Interactable
{
    private InventoryScript inventoryScript;
    private bool fixedCheck = false;
    public int itemsNeeded = 3;
    public GameObject[] thingsToTurnOn;
    public GameObject[] thingsToTurnOff;
    void Interact()
    {
        //Do interact code here

        Debug.Log("Gen Check!");
        inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

        if (inventoryScript.generatorItems < itemsNeeded && !fixedCheck)
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
            inventoryScript.generatorItems = 0;
            for (int i = 0; i < thingsToTurnOn.Length; i++)
            {
                thingsToTurnOn[i].SetActive(true);
            }
            for (int i = 0; i < thingsToTurnOff.Length; i++)
            {
                thingsToTurnOff[i].SetActive(false);
            }
        }
    }
}
