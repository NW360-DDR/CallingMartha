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
            if (gameObject.tag == "Window")
            {
                AudioManager.PlayAxeImpactGlass();
            }

            if (gameObject.tag == "Lock")
            {
                AudioManager.PlayAxeImpactMetal();
            }

            if (gameObject.tag == "Barrel")
            {
                AudioManager.PlayAxeImpactWood();
            }
            Destroy(gameObject);
        }
    }
}
