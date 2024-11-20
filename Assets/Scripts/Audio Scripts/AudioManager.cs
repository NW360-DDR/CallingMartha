using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  

    // Start is called before the first frame update


    

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Woves/Play_WovesHurt");
    }

   
}
