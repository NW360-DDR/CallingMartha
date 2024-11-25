using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
//For right now this is going to look like a mess of single line classes that do the same thing over and over again, but with different sounds.
//Maybe I'll have time to go back and make this look good or do things better, but for right now I just need it to do things.
//Also not sure when they made it I didn't need a using FMODorsomethingrather statement above this, but I'm not going to question it.
{

    public FMODUnity.EventReference PlayerHurtSound;
    public FMODUnity.EventReference PlayerDeadSound;
    public FMODUnity.EventReference WolfHurtSound;
    public FMODUnity.EventReference WolfAttackSound;
    public FMODUnity.EventReference PhoneNotificationSound;
    public FMODUnity.EventReference MarthaVoicemailSound;
    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerHurt()
    {
        FMODUnity.RuntimeManager.PlayOneShot(PlayerHurtSound);
    }

    public void PlayerDead()
    {
        FMODUnity.RuntimeManager.PlayOneShot(PlayerDeadSound);
    }

    public void WolfHurt()
    {
        FMODUnity.RuntimeManager.PlayOneShot(WolfHurtSound);
    }

    public void WolfAttack()
    {
        FMODUnity.RuntimeManager.PlayOneShot(WolfAttackSound);
    }

    public void PhoneNotification()
    {
        Debug.Log("WITNESS ME!!!!! MOTOROLA!!!!!");
        FMODUnity.RuntimeManager.PlayOneShot(PhoneNotificationSound);

    }

    public void MarthaVoicemail()
    {
        FMODUnity.RuntimeManager.PlayOneShot(MarthaVoicemailSound);
    }
}
