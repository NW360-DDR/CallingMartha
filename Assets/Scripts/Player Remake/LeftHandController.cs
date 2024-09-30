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

    // Update is called once per frame
    void Update()
    {
        //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        //controls what key is bound to what is equipped for left hand
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentEqupped != 0)
        {
            currentEqupped = 0;
            leftHand.GetComponent<Animator>().SetTrigger("SwitchingHand");
            GetComponent<RockThrowScript>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentEqupped != 1 && GetComponentInChildren<GrabAndThrow>().rockCount >= 1)
        {
            currentEqupped = 1;
            leftHand.GetComponent<Animator>().SetTrigger("SwitchingHand");
            GetComponent<RockThrowScript>().enabled = true;
        }

        //if the player runs out of rocks, automatically switch to the first equip slot
        if (GetComponentInChildren<GrabAndThrow>().rockCount == 0 && currentEqupped == 1)
        {
            currentEqupped = 0;
            leftHand.GetComponent<Animator>().SetTrigger("SwitchingHand");
            GetComponent<RockThrowScript>().enabled = false;
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
