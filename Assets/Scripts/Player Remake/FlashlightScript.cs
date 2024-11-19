using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    public GameObject flashlightObject;
    private Light flashlightLight;

    private InventoryScript InventoryScript;
    private PhoneHandler phoneHandler;

    public bool flashlightOn = false;
    public float batteryLife = 100;
    public bool updatedBatteries = false;
    public bool flickered = false;

    // Start is called before the first frame update
    void Start()
    {
        InventoryScript = GetComponent<InventoryScript>();
        phoneHandler = GameObject.Find("PhoneCanvas").GetComponent<PhoneHandler>();
        flashlightLight = flashlightObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        flashlightLight.intensity = phoneHandler.phoneBatteryLife * 1.5f;

        //if the flashlight is not on, turn it on. if it is on, turn it off
        if (Input.GetMouseButtonDown(0) && InventoryScript.flashLightBatteries > 0 && phoneHandler.phoneBatteryLife > 0 && !flashlightOn && Time.timeScale > 0)
        {
            flashlightObject.SetActive(true);
            flashlightOn = true;
        }else if (Input.GetMouseButtonDown(0) && flashlightOn && Time.timeScale > 0)
        {
            flashlightObject.SetActive(false);
            flashlightOn = false;
        }

        //drain flashlight battery
        /*if (flashlightOn && batteryLife > 0)
        {
            batteryLife -= Time.deltaTime * 2;
        }

        //if the flashlight battery life reaches 0, get rid of a battery and replace with next one if the player has one
        if (batteryLife <= 0 && !updatedBatteries)
        {
            InventoryScript.flashLightBatteries -= 1;
            updatedBatteries = true;

            //this part replaces if the player has another
            if (InventoryScript.flashLightBatteries > 0)
            {
                batteryLife = 100;
                flickered = false;
                updatedBatteries = false;
            }
        }*/

        if (phoneHandler.phoneBatteryLife <= 0)
        {
            Debug.Log("Battery life gone!");
            flashlightObject.SetActive(false);
            flashlightOn = false;
        }

        //automatically turn off flashlight if the battery reaches 0 and there are no batteries left
        if (batteryLife <= 0 && InventoryScript.flashLightBatteries == 0)
        {
            flashlightObject.SetActive(false);
            flashlightOn = false;
        }

        //if flashlight battery hits 10, flicker
        if (batteryLife <= 10 && !flickered)
        {
            StartCoroutine(FlashLightFlicker());
        }

        IEnumerator FlashLightFlicker()
        {
            flickered = true;
            for (int i = 0; i < 4; i++)
            {
                Debug.Log("Flickering!");
                flashlightObject.SetActive(false);
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                flashlightObject.SetActive(true);
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
            }
        }
    }
}
