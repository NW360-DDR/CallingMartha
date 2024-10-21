using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeSpriteScript : MonoBehaviour
{
    // this is to send a signal to the axeslash script to actually attack
    void SendAttack()
    {
        GameObject.Find("Player (Remake)").GetComponent<AxeSlash>().attackSignal = true;
    }

    //this is called in an animation event when the attack ends
    void EndAttack()
    {
        GameObject.Find("Player (Remake)").GetComponent<AxeSlash>().attackSignal = false;
    }

    //this is called in an animation event when idle starts
    void ResetInput()
    {
        GetComponent<Animator>().ResetTrigger("IsAttacking");
        GameObject.Find("Player (Remake)").GetComponent<AxeSlash>().takeInput = true;
    }
}
