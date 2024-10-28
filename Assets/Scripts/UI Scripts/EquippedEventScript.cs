using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedEventScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndSwitch()
    {
        GetComponent<Animator>().SetBool("SwitchingHand", false);
    }
}
