using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicelineDirectory : MonoBehaviour
{
    public FMODUnity.EventReference InGame3Event;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InGame3()
    {
        FMODUnity.RuntimeManager.PlayOneShot(InGame3Event);
    }
}
