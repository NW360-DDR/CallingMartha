using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource BaseAmbiSource;
    public AudioClip BaseAmbi;
    public AudioClip BaseAmbi2;

    // Start is called before the first frame update
    void Start()
    {
        BaseAmbiSource = GetComponent<AudioSource>();
        BaseAmbi = BaseAmbiSource.clip;
        Debug.Log("Audio Clip Length : " + BaseAmbiSource.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchAudio();
        }
    }

    void SwitchAudio()
    {
        if (BaseAmbiSource.clip = BaseAmbi)
        {
            BaseAmbiSource.clip = BaseAmbi2;
            BaseAmbiSource.Play();
        }

        else if (BaseAmbiSource.clip = BaseAmbi2)
        {
            BaseAmbiSource.clip = BaseAmbi;
            BaseAmbiSource.Play();
        }
    }
}
