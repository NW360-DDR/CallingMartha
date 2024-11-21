using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedEventScript : MonoBehaviour
{
    private EquippedScript equipScript;
    // Start is called before the first frame update
    void Start()
    {
        equipScript = GetComponentInParent<EquippedScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndSwitch()
    {
        GetComponent<Animator>().SetBool("SwitchingHand", false);
    }

    void AllowAttack()
    {
        equipScript.allowAttack = true;
    }
}
