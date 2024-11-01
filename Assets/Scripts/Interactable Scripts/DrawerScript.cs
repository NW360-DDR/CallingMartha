using UnityEngine;

public class DrawerScript : Interactable
{
    private Animator drawerAnimator;
    private bool drawOpen = false;
    void Interact()
    {
        //Do interact code here

        drawerAnimator = GetComponent<Animator>();
        drawerAnimator.SetBool("Open", !drawerAnimator.GetBool("Open"));
        drawOpen = !drawOpen;
    }
}
