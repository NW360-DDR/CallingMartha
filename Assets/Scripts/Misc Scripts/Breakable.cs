using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public AudioManager AudioManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AxeHitbox"))
        {
            AudioManager.PlayAxeImpactWood();
            Destroy(gameObject);
        }
    }
}
