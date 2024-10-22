using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameVOScriptMod : MonoBehaviour
{

    public AudioSource InGameClip;
    public float ClipLength;
    public Collider BoxCollider;

    public AudioManager AudioManager;
    // Start is called before the first frame update
 
    public void OnTriggerExit(Collider other)
    {
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
        yield return new WaitForSeconds(ClipLength);
        AudioManager.AudioSourcesPlaying = 0;
        Destroy(this.gameObject);
    }
}
