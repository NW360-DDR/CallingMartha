using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    public GameObject flashlightObject;

    private InventoryScript InventoryScript;

    public bool flashlightOn = false;
    public float batteryLife = 100;
    public bool updatedBatteries = false;
    public bool flickered = false;

    public GameObject leftHand;

    public Sprite flashLightOff;
    public Sprite flashLightOn;

    // Start is called before the first frame update
    void Start()
    {
        InventoryScript = GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the flashlight is not on, turn it on. if it is on, turn it off
        if (Input.GetMouseButtonDown(1) && InventoryScript.flashLightBatteries > 0 && !flashlightOn)
        {
            flashlightObject.SetActive(true);
            flashlightOn = true;
            leftHand.GetComponent<Image>().sprite = flashLightOn;
        }else if (Input.GetMouseButtonDown(1) && flashlightOn)
        {
            flashlightObject.SetActive(false);
            flashlightOn = false;
            leftHand.GetComponent<Image>().sprite = flashLightOff;
        }

        //drain flashlight battery
        if (flashlightOn && batteryLife > 0)
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
        }

        //automatically turn off flashlight if the battery reaches 0 and there are no batteries left
        if (batteryLife <= 0 && InventoryScript.flashLightBatteries == 0)
        {
            flashlightObject.SetActive(false);
            flashlightOn = false;
            leftHand.GetComponent<Image>().sprite = flashLightOff;
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
                leftHand.GetComponent<Image>().sprite = flashLightOff;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                flashlightObject.SetActive(true);
                leftHand.GetComponent<Image>().sprite = flashLightOn;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
            }
        }
    }
}
