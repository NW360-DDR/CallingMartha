using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class wolfTutorial : TutorialTrigger
{
    //Do whatever is needed to trigger tutorial

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wolf"))
        {
            tutorialString = "Dodge enemies - Shift \n Attack enemies - Left Click";
            SendLog();
        }else if (other.gameObject.CompareTag("Checkpoint"))
        {
            tutorialString = "Use light to keep wolves away";
            SendLog();
        }
        else if (other.gameObject.CompareTag("Barrel"))
        {
            tutorialString = "Break objects with axe.";
            SendLog();
        }
    }
}
