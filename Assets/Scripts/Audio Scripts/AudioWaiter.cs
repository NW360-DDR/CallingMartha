using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWaiter : MonoBehaviour
{
    public AudioSource InGame5;
    public float Delay;
    public float ClipLength;
    public Collider BoxCollider;

    public AudioManager AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
        {
            StartCoroutine(AudioWait());
        }
    }

    public IEnumerator AudioWait()
    {
        yield return new WaitForSeconds(Delay);
        InGame5.enabled = true;
        StartCoroutine(ClipRuntime());
    }

    public IEnumerator ClipRuntime()
    {
        Debug.Log("I'm Playing!!!");
        InGame5.Play();
        AudioManager.AudioPlaying = true;
        AudioManager.AudioSourcesPlaying += 1;
        yield return new WaitForSeconds(ClipLength);
        AudioManager.AudioSourcesPlaying -= 1;
        Destroy(this.gameObject);
    }
}
