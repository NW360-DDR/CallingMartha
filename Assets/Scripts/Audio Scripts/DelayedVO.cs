using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedVO : MonoBehaviour
{
    public AudioManager AudioManager;
    public int delay;
    public int VoiceOverIndex;

    public void OnTriggerEnter (Collider other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("Youre hearing voices Michael");
            StartCoroutine(DelayedVoice());
        }
    }

    public IEnumerator DelayedVoice()
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Voiceline(VoiceOverIndex);
    }
}
