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

    private void FixedUpdate()
    {
        if(Random.Range(0f, 1f) <= 0.001f && !soundPlayer.isPlaying)
        {
            soundPlayer.Stop();
            soundPlayer.PlayOneShot(Voicemails[3]);
        }
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
