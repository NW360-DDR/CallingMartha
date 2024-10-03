using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LeftHandController : MonoBehaviour
{
    public Sprite[] items;
    public GameObject leftHand;
    public int currentEqupped = 0;

    public bool switchHandSignal = false;

    private GrabAndThrow grabScript;
    private RockThrowScript rockScript;
    private FlashlightScript lightScript;

    private Animator leftHandAnim;

    private void Start()
    {
        grabScript = GetComponentInChildren<GrabAndThrow>();
        rockScript = GetComponent<RockThrowScript>();
        lightScript = GetComponent<FlashlightScript>();
        leftHandAnim = leftHand.GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentEqupped != 1 && grabScript.rockCount >= 1)
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
        if (GetComponentInChildren<GrabAndThrow>().rockCount == 0 && currentEqupped == 1)
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
