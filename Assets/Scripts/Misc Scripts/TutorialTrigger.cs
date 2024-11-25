using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    protected string tutorialString;

    TextLogThingy textLog;
    // Start is called before the first frame update
   private void Start()
    {
        textLog = GameObject.FindAnyObjectByType<TextLogThingy>();
    }

    public void SendLog()
    {
        textLog.TextPush(tutorialString);
    }

    //Do whatever is needed to trigger tutorial
}
