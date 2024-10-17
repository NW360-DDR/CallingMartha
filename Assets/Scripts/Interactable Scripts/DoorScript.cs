using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactable
{
    private Animator doorAnimator;
    private bool doorOpen = false;
    void Interact()
    {
        //Do interact code here

        doorAnimator = GetComponentInParent<Animator>();

        if (!doorOpen)
        {
            doorAnimator.SetTrigger("Open");
            doorOpen = true;
        }
        else
        {
            doorAnimator.SetTrigger("Close");
            doorOpen = false;
        }
    }
}
