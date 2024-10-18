using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class LeftHandController : MonoBehaviour
{
    public Sprite[] items;
    public GameObject leftHand;
    public GameObject phone;
    public GameObject axe;
    public int currentEqupped = 0;

    public bool switchHandSignal = false;
    public bool phoneOpen = false;

    private InventoryScript inventoryScript;
    private RockThrowScript rockScript;
    private FlashlightScript lightScript;
    private AxeSlash axeScript;

    private Animator leftHandAnim;
    private Animator phoneAnim;
    private Animator axeAnim;

    private void Start()
    {
        inventoryScript = GetComponent<InventoryScript>();
        rockScript = GetComponent<RockThrowScript>();
        lightScript = GetComponent<FlashlightScript>();
        leftHandAnim = leftHand.GetComponent<Animator>();
        phoneAnim = phone.GetComponent<Animator>();
        axeAnim = axe.GetComponent<Animator>();
        axeScript = GetComponent<AxeSlash>();
    }

    // Update is called once per frame
    void Update()
    {
        //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        //controls what key is bound to what is equipped for left hand
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentEqupped != 0)
        {
            currentEqupped = 0;
            leftHandAnim.SetTrigger("SwitchingHand");
            rockScript.enabled = false;
            lightScript.enabled = true;
        }

        //take phone out and put axe away if player hits tab and is able to
        if (Input.GetKeyDown(KeyCode.Tab) && !phoneOpen && axeScript.takeInput && !axeAnim.GetBool("HoldingDown"))
        {
            phoneAnim.SetBool("Phone Up", true);
            axeAnim.SetBool("Lowered", true);
            axeScript.takeInput = false;
            phoneOpen = true;
        }else if (Input.GetKeyDown(KeyCode.Tab) && phoneOpen)
        {
            phoneAnim.SetBool("Phone Up", false);
            axeAnim.SetBool("Lowered", false);
            phoneOpen = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentEqupped != 1 && inventoryScript.rockCount >= 1)
        {
            currentEqupped = 1;
            leftHandAnim.SetTrigger("SwitchingHand");
            rockScript.enabled = true;
            lightScript.flashlightObject.SetActive(false);
            lightScript.flashlightOn = false;
            lightScript.enabled = false;
            lightScript.leftHand.GetComponent<Image>().sprite = lightScript.flashLightOff;
        }

        //if the player runs out of rocks, automatically switch to the first equip slot
        if (inventoryScript.rockCount == 0 && currentEqupped == 1)
        {
            currentEqupped = 0;
            leftHandAnim.SetTrigger("SwitchingHand");
            rockScript.enabled = false;
            lightScript.enabled = true;
        }

        //if recieving signal from the UI gods, run the change hand method
        if (switchHandSignal)
        {
            ChangeCurrentEquipped();
        }
    }

    void ChangeCurrentEquipped()
    {
        Debug.Log("Switching!");
        leftHand.GetComponent<Image>().sprite = items[currentEqupped];
        switchHandSignal = false;
    }
}
