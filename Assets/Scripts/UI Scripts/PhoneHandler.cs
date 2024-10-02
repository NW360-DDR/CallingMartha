using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneHandler : MonoBehaviour
{
    enum Screen {GPS, VM, Log, HUD};
    [SerializeField] GameObject GPSMode;
    [SerializeField] GameObject VoicemailMode;
    [SerializeField] GameObject LogMode;
    [SerializeField] GameObject HUDMode;

    CellService cell;

    // Start is called before the first frame update, used to find anything we need in our Components/
    void Start()
    {
        cell = GetComponentInChildren<CellService>();
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
