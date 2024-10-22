using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame3Start : MonoBehaviour
{
    //Sets everything to play audio
    public AudioSource InGameClip;
    public float ClipLength;
    public Collider BoxCollider;

    //tells Audio Manager that audio is playing
    public AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
       

    }


    // Update is called once per frame
    void Update()
    {

        
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Says to play audio when player enters area
        if (other.tag == "Player")
        {
            InGameClip.Play();
            AudioManager.AudioPlaying = true;
            AudioManager.AudioSourcesPlaying += 1;
            StartCoroutine(WaittoEnd());
            BoxCollider.enabled = false;
            
        }
    }

    public IEnumerator WaittoEnd()
    {
        // Gives object enough time to play the audio, wait a second, then die.
        yield return new WaitForSeconds(ClipLength);
        AudioManager.AudioSourcesPlaying = 0;
        Destroy(this.gameObject);
    }
}
