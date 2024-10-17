using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactable
{
    private Animator doorAnimator;
    void Interact()
    {
        //Do interact code here

        doorAnimator = GetComponentInParent<Animator>();

        doorAnimator.SetTrigger("Open");
    }
}
