using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public bool AudioPlaying = false;

    private AudioSource[] allAudioSources;
    public int AudioSourcesPlaying = 0;

    // Start is called before the first frame update


    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (AudioPlaying == true)
        {

            if (AudioSourcesPlaying > 1)
            {
                StopAllAudio();
            }

        }
    }

   
}
