using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class DrawerScript : Interactable
{
    private Animator drawerAnimator;
    private bool drawOpen = false;
    void Interact()
    {
        //Do interact code here

        drawerAnimator = GetComponent<Animator>();

        if (!drawOpen)
        {
            drawerAnimator.SetBool("Open", true);
            drawOpen = true;
        }
        else
        {
            drawerAnimator.SetBool("Open", false);
            drawOpen = false;
        }
    }
}
