using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneHandler : MonoBehaviour
{
    enum Screen {HUD, Save};
    [SerializeField] GameObject SaveMode;
    [SerializeField] GameObject HUDMode;
    [SerializeField] TextMeshProUGUI rockText, battText, kitText;
    CellService cell;
    public string playerName;
    Player player;
    byte[] inventoryTemp;
    Screen currentScreen = Screen.HUD;
    // Start is called before the first frame update, used to find anything we need in our Components/
    void Start()
    {
        cell = GetComponentInChildren<CellService>();
        if (string.IsNullOrWhiteSpace(playerName))
            player = new Player();
        else
            player = new Player(playerName);
        inventoryTemp = new byte[3];
    }

    private void FixedUpdate()
    {
        inventoryTemp = player.GetInventory(); // Remember, Rocks, Batts, Kits.
        rockText.text = inventoryTemp[0].ToString() + " rocks";
        battText.text = inventoryTemp[1].ToString() + " batteries";
        kitText.text = inventoryTemp[2].ToString() + " medkits";
    }
    void SwitchMode(Screen newMode)
    {
        HUDMode.SetActive(false);
        switch (newMode) 
        {

            case Screen.HUD:
                HUDMode.SetActive(true);
                break;
            default:
                break;
        }
        currentScreen = newMode;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            cell.ServiceUpdate(other.GetComponent<CellVolume>().cellPower);
        }
    }
}

/// <summary>
/// Converts most of the information from the player's various scripts into a single, easy to parse class.
/// </summary>
public class Player{
     readonly GameObject main;
     public InventoryScript inventory;
    public Player() 
    {
        Debug.Log("No name specified, default constructor."); 
        main = GameObject.FindWithTag("Player");
        inventory = main.GetComponentInChildren<InventoryScript>();
        
    }
    public Player(string playerName)
    {
        main = GameObject.Find(playerName);
        inventory = main.GetComponentInChildren<InventoryScript>();
    }

    public byte[] GetInventory() // Returns the players Rocks, Batteries, and Medkit counts for the UI.
    {
        byte[] temp = {3, 4, 5 }; // Rocks, Batts, Kits
       temp[0] = (byte)inventory.rockCount;
       temp[1] = (byte)inventory.flashLightBatteries;
       temp[2] = (byte)inventory.medKitCount;

        return temp;
    }
}