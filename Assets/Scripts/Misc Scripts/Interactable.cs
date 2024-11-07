using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour 
{
    protected string logString;
    TextLogThingy textLog;

    private void Start()
    {
        textLog = GameObject.FindAnyObjectByType<TextLogThingy>();
    }
    void Interact()
    {
        //Do interact code here
    }

    public void SendLog()
    {
        textLog.TextPush(logString);
    }
}
