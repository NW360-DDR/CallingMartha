using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugHealth : MonoBehaviour
{
    private HealthAndRespawn healthScript;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = GameObject.Find("Player (Remake)").GetComponent<HealthAndRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "health: " + healthScript.health;
    }
}
