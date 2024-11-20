using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  //For right now this is going to look like a mess of single line classes that do the same thing over and over again, but with different sounds.
  //Maybe I'll have time to go back and make this look good or do things better, but for right now I just need it to do things.
  //Also not sure when they made it I didn't need a using FMODorsomethingrather statement above this, but I'm not going to question it.


    

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayerHurt()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Michael/Play_MichaelHurt");
    }

    public void PlayerDead()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Michael/PlayMichaelDead");
    }

    public void WolfHurt()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Wolves/Play_WolvesHurt");
    }

    public void WolfAttack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event/SFX/Wolves/Play_WolvesAttack");
    }

   
}
