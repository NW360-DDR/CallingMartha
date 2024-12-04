using UnityEngine;

public class DrawerScript : Interactable
{
    private Animator drawerAnimator;
    private bool drawOpen = false;

    public AudioManager AudioManager;
    void Interact()
    {
        //Do interact code here

        drawerAnimator = GetComponent<Animator>();
        drawerAnimator.SetBool("Open", !drawerAnimator.GetBool("Open"));
        AudioManager.PlayDrawerSound();
        drawOpen = !drawOpen;
    }
}
