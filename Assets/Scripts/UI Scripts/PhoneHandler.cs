using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneHandler : MonoBehaviour
{
    // Screen Management Variables
    enum Screen {HUD, Save, Off, Call};
    Screen currentScreen = Screen.HUD;
    [SerializeField] GameObject SaveMode;
    [SerializeField] GameObject HUDMode;
    [SerializeField] GameObject OffMode;
    [SerializeField] GameObject CallMode;
    // Text and other data information
    [SerializeField] TextMeshProUGUI rockText, battText, kitText, gasText;
    [SerializeField] TextMeshProUGUI SaveText;
    [SerializeField] CellService cell;
    public string playerName;
    public float phoneBatteryLife = 100;
    public Slider batterySlider;
    bool canSave = false;
    bool hasSaved = false;
    public bool gettingCall = false;

    public AudioManager AudioManager;
    public GameObject VoiceMail;
    public FMODUnity.StudioEventEmitter VoicemailEmmiter;

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
        inventoryTemp = new byte[4];
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
        else if(gettingCall)
        {
            SwitchMode(Screen.Call);

        }
        else if (!gettingCall && currentScreen == Screen.Call)
        {
            SwitchMode(Screen.HUD);
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

        if (gettingCall && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Play call!");
            VoiceMail.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        batterySlider.value = phoneBatteryLife;

        // ~~~~~~~~~~~~~~~~~~~~~ Screen Updating ~~~~~~~~~~~~~~~~~~~~~~
        if (currentScreen == Screen.HUD)
        {
            inventoryTemp = player.GetInventory(); // Remember, Rocks, Batts, Kits.
            rockText.text = inventoryTemp[0].ToString() + " bullets";
            battText.text = inventoryTemp[1].ToString() + " lighters";
            kitText.text = inventoryTemp[2].ToString() + " medkits";
            gasText.text = inventoryTemp[3].ToString() + " gas cans";
        }
        else if (currentScreen == Screen.Save)
        {
            if(cell.service == 3 && !hasSaved) // Maximum Cell Service only while we have not Checkpointed
            {
                SaveText.text = "Connection Established";
                SaveText.GetComponentInParent<RawImage>().color = Color.green;
                canSave = true;
            }
            else if (cell.service == 3 && hasSaved)
            {
                SaveText.text = "Location Logged.";
                StartCoroutine(SetTextBack());
            }
            else
            {
                SaveText.text = "Unstable Connection";
                SaveText.GetComponentInParent<RawImage>().color = Color.red;
                canSave = false;
            }
        }
    }
    IEnumerator SetTextBack()
    {
        if (currentScreen == Screen.Save)
        {
            yield return new WaitForSeconds(1);
            hasSaved = false;
            SaveText.text = "Connection Established";
        }  
    }
    void SwitchMode(Screen newMode)
    {
        HUDMode.SetActive(false);
        SaveMode.SetActive(false);
        CallMode.SetActive(false);
        switch (newMode) 
        {
            case Screen.HUD:
                HUDMode.SetActive(true);
                break;
            case Screen.Save:
                SaveMode.SetActive(true);
                break;
            case Screen.Call:
                CallMode.SetActive(true);
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
        byte[] temp = {3, 4, 5, 99 }; // Rocks, Batts, Kits
        temp[0] = (byte)inventory.bulletCount;
        temp[1] = (byte)inventory.lighters;
        temp[2] = (byte)inventory.medKitCount;
        temp[3] = (byte)inventory.generatorItems;

        return temp;
    }

}