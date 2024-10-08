using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceMail : MonoBehaviour
{
    
    [SerializeField] AudioClip[] Voicemails;
    public AudioSource soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }


    public void PlayVM(int index)
    {
        if (index >= 0 && index < Voicemails.Length)
        {
            soundPlayer.Stop();
            soundPlayer.PlayOneShot(Voicemails[index]);
        }
    }
}
