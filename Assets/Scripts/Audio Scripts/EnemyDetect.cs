using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public MusicTransition MusicTransition;

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Wolf")
        {
            MusicTransition.SwitchToEncounter();
        }
    }
}
