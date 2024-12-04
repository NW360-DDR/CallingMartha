using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public AudioManager AudioManager;
    private void OnTriggerEnter(Collider other)
    {

        if (gameObject.tag == "Window")
        {
            AudioManager.PlayAxeImpactGlass();
        }

        if (gameObject.tag == "Lock")
        {
            AudioManager.PlayAxeImpactMetal();
        }

        if (gameObject.tag != "Window" && gameObject.tag != "Lock")
        {
            AudioManager.PlayAxeImpactWood();
        }
        if (other.gameObject.CompareTag("AxeHitbox"))
        {
            Destroy(gameObject);
        }
    }
}
