using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeSpriteScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SendAttack()
    {
        GameObject.Find("Player (Remake)").GetComponent<AxeSlash>().attackSignal = true;
    }

    void EndAttack()
    {
        GameObject.Find("Player (Remake)").GetComponent<AxeSlash>().attackSignal = false;
    }

    void ResetInput()
    {
        GameObject.Find("Player (Remake)").GetComponent<AxeSlash>().takeInput = true;
    }
}
