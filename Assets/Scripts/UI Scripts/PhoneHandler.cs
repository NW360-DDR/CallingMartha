using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneHandler : MonoBehaviour
{
    KeyCode Z = KeyCode.Z, C = KeyCode.C;
    enum Screen {GPS, VM, Log, HUD};
    [SerializeField] GameObject GPSMode;
    [SerializeField] GameObject VoicemailMode;
    [SerializeField] GameObject LogMode;
    [SerializeField] GameObject HUDMode;
    [SerializeField] RawImage HPBar, BattBar;
    float barWidth;
    CellService cell;
    public string playerName;
    Player player;

    // Start is called before the first frame update, used to find anything we need in our Components/
    void Start()
    {
        cell = GetComponentInChildren<CellService>();
        barWidth = HPBar.rectTransform.rect.width;
        if (string.IsNullOrWhiteSpace(playerName))
            player = new();
        else
            player = new(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchMode(Screen mode)
    {
        GPSMode.SetActive(false);
        VoicemailMode.SetActive(false);
        LogMode.SetActive(false);
        HUDMode.SetActive(false);
        switch (mode) 
        {
            case Screen.GPS:
                GPSMode.SetActive(true);
                break;
            case Screen.VM:
                VoicemailMode.SetActive(true);
                break;
            case Screen.Log:
                LogMode.SetActive(true);
                break;
            case Screen.HUD:
                HUDMode.SetActive(true);
                break;
            default:
                break;
        }
    }
}

public class Player{
    GameObject main;
    public GrabAndThrow inventory;
    public HealthAndRespawn health;

    public Player() 
    {
        main = GameObject.FindWithTag("Player");
        inventory = main.GetComponent<GrabAndThrow>();
        health = main.GetComponent<HealthAndRespawn>();
    }
    public Player(string playerName)
    {
        main = GameObject.Find(playerName);
        inventory = main.GetComponent<GrabAndThrow>();
        health = main.GetComponent<HealthAndRespawn>();

         int GetHealth()
        {
            return (int)(health.health); // Why is health a float? Do we need decimal accuracy when we only add or remove in integer increments?
        }

        int[] GetInventory()
        {
            int[] temp =  {3, 4, 5}; // Rocks, Batts, Kits
            temp[0] = (int)inventory.rockCount; // WHO HOLDS A DECIMAL NUMBER OF ROCKS
            // temp[1] = (int)inventory.batteryCount; // okay this one is almost debatable but what the fuck // This isn't in my version of the script yet, roll with it.
            temp[2] = (int)inventory.medKitCount; // WE AREN'T USING HALF A MEDKIT

            return temp;
        }
    }
}