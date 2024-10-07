using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneHandler : MonoBehaviour
{
   readonly KeyCode Z = KeyCode.Z, C = KeyCode.C;
    enum Screen {GPS, VM, Log, HUD};
    [SerializeField] GameObject GPSMode;
    [SerializeField] GameObject VoicemailMode;
    [SerializeField] GameObject LogMode;
    [SerializeField] GameObject HUDMode;
    [SerializeField] RawImage HPBar, BattBar;
    float barWidth;
    [SerializeField] TextMeshProUGUI rockText, battText, kitText;
    CellService cell;
    public string playerName;
    Player player;
    int[] temp;
    // Start is called before the first frame update, used to find anything we need in our Components/
    void Start()
    {
        cell = GetComponentInChildren<CellService>();
        barWidth = HPBar.rectTransform.rect.width;
        if (string.IsNullOrWhiteSpace(playerName))
            player = new Player();
        else
            player = new Player(playerName);
        temp = new int[3];
    }

    void FixedUpdate()
    {
        temp = player.GetInventory(); // Remember, Rocks, Batts, Kits.
        rockText.text = temp[0].ToString();
        battText.text = temp[1].ToString();
        kitText.text = temp[2].ToString();
        HPBar.rectTransform.sizeDelta = new(barWidth * (player.GetHealth()/100f), HPBar.rectTransform.sizeDelta.y);
        BattBar.rectTransform.sizeDelta = new(barWidth * (player.GetBatt() / 100f), BattBar.rectTransform.sizeDelta.y);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            cell.ServiceUpdate(other.GetComponent<CellVolume>().cellPower);
        }
    }
}

public class Player{
     readonly GameObject main;
     public GrabAndThrow inventory;
     readonly HealthAndRespawn health;
    readonly FlashlightScript light;
    

    public Player() 
    {
        Debug.Log("No name specified, default constructor.");
        main = GameObject.FindWithTag("Player");
        inventory = main.GetComponentInChildren<GrabAndThrow>();
        health = main.GetComponent<HealthAndRespawn>();
        light = main.GetComponent<FlashlightScript>();
        
    }
    public Player(string playerName)
    {
        main = GameObject.Find(playerName);
        inventory = main.GetComponentInChildren<GrabAndThrow>();
        health = main.GetComponent<HealthAndRespawn>();
        light = main.GetComponent<FlashlightScript>();
    }
    public int GetHealth()
    {
        return health.health; // Why is health a float? Do we need decimal accuracy when we only add or remove in integer increments?
    }

    public float GetBatt()
    {
        return light.batteryLife;
    }

    public int[] GetInventory()
    {
        int[] temp = { 3, 4, 5 }; // Rocks, Batts, Kits
        temp[0] = inventory.rockCount; // WHO HOLDS A DECIMAL NUMBER OF ROCKS
        temp[1] = inventory.flashLightBatteries; // okay this one is almost debatable but what the fuck // This isn't in my version of the script yet, roll with it.
        temp[2] = inventory.medKitCount; // WE AREN'T USING HALF A MEDKIT

        return temp;
    }
}