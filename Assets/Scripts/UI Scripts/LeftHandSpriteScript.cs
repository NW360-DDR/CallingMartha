using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandSpriteScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SendSwitchSignal()
    {
        GameObject.Find("Player (Remake)").GetComponent<LeftHandController>().switchHandSignal = true;
    }
}
