using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneHandler : MonoBehaviour
{
    // Screen Management Variables
    enum Screen {HUD, Save, Off};
    Screen currentScreen = Screen.HUD;
    [SerializeField] GameObject SaveMode;
    [SerializeField] GameObject HUDMode;
    [SerializeField] GameObject OffMode;
    // Text and other data information
    [SerializeField] TextMeshProUGUI rockText, battText, kitText;
    [SerializeField] TextMeshProUGUI SaveText;
    [SerializeField] CellService cell;
    public string playerName;
    public float phoneBatteryLife = 100;
    bool canSave = false;
    bool hasSaved = false;

    // These two are for getting the inventory. Why did I do it this way? I don't know, it made sense at the time.
    Player player;
    byte[] inventoryTemp;
    
    // Start is called before the first frame update, used to find anything we need in our Components/
    void Start()
    {
        if (string.IsNullOrWhiteSpace(playerName))
            player = new Player();
        else
            player = new Player(playerName);
        inventoryTemp = new byte[3];
        player.healthScript.checkpoint = player.main.transform.position;
    }

    private void Update()
    {
        // ~~~~~~~~~~~~~~~~~~~ Screen Changing ~~~~~~~~~~~~~~~~~~~~~~~~
        if (Input.GetKeyDown(KeyCode.Z) && currentScreen != Screen.HUD) // Positive values up, negative down
        {// Positive means we want to see the HUD if we aren't already
            SwitchMode(Screen.HUD);
        }
        else if (Input.GetKeyDown(KeyCode.C) && currentScreen != Screen.Save) // Positive values up, negative down
        {// Positive means we want to see the Save Menu if we aren't already
            SwitchMode(Screen.Save);
        }
        else if(phoneBatteryLife <= 0)
        {
            SwitchMode(Screen.Off);
        }
        // ~~~~ Checking for Checkpoints
        if (canSave && !hasSaved && Input.GetKeyDown(KeyCode.C)) // Can we save? Have we not yet saved? Did we hit the button
        {
            hasSaved = true;
            // Maddie put the things here for checkpointing.
            // You got it

            player.healthScript.checkpoint = player.main.transform.position;
        }
    }
    private void FixedUpdate()
    {
        
        // ~~~~~~~~~~~~~~~~~~~~~ Screen Updating ~~~~~~~~~~~~~~~~~~~~~~
        if (currentScreen == Screen.HUD)
        {
            inventoryTemp = player.GetInventory(); // Remember, Rocks, Batts, Kits.
            rockText.text = inventoryTemp[0].ToString() + " bullets";
            battText.text = inventoryTemp[1].ToString() + " lighters";
            kitText.text = inventoryTemp[2].ToString() + " medkits";
        }
        else if (currentScreen == Screen.Save)
        {
            if(cell.service == 3 && !hasSaved) // Maximum Cell Service only while we have not Checkpointed
            {
                SaveText.text = "Connection Established";
                canSave = true;
            }
            else if (cell.service == 3 && hasSaved)
            {
                SaveText.text = "Location Logged.";
            }
            else
            {
                SaveText.text = "Unstable Connection";
                canSave = false;
            }
        }
        
    }
    void SwitchMode(Screen newMode)
    {
        HUDMode.SetActive(false);
        SaveMode.SetActive(false);
        switch (newMode) 
        {
            case Screen.HUD:
                HUDMode.SetActive(true);
                break;
            case Screen.Save:
                SaveMode.SetActive(true);
                break;
            case Screen.Off:
                OffMode.SetActive(true);
                break;
            default:
                break;
        }
        currentScreen = newMode;
        hasSaved = false;
    }

    
}

/// <summary>
/// Converts most of the information from the player's various scripts into a single, easy to parse class.
/// </summary>
public class Player{
     readonly public GameObject main;
     public InventoryScript inventory;
     public HealthAndRespawn healthScript;
    public Player() 
    {
        healthScript = main.GetComponent<HealthAndRespawn>();
        Debug.Log("No name specified, default constructor."); 
        main = GameObject.FindWithTag("Player");
        inventory = main.GetComponentInChildren<InventoryScript>();
        
    }
    public Player(string playerName)
    {
        main = GameObject.Find(playerName);
        inventory = main.GetComponentInChildren<InventoryScript>();
        healthScript = main.GetComponent<HealthAndRespawn>();
    }

    public byte[] GetInventory() // Returns the players Rocks, Batteries, and Medkit counts for the UI.
    {
        byte[] temp = {3, 4, 5 }; // Rocks, Batts, Kits
        temp[0] = (byte)inventory.bulletCount;
        temp[1] = (byte)inventory.lighters;
        temp[2] = (byte)inventory.medKitCount;

        return temp;
    }
}