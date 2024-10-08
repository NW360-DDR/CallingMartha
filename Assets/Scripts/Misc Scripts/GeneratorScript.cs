using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    private GrabAndThrow grabScript;
    private bool fixedCheck = false;
    public GameObject[] thingsToTurnOn;
    void GeneratorCheck()
    {
        Debug.Log("Gen Check!");
        grabScript = GameObject.Find("Player (Remake)").GetComponentInChildren<GrabAndThrow>();

        if (grabScript.generatorItems < 3 && !fixedCheck)
        {
            Debug.Log("Sorry! Can't fix!");
        }else
        {
            fixedCheck = true;
            for (int i = 0; i < thingsToTurnOn.Length; i++)
            {
                thingsToTurnOn[i].SetActive(true);
            }
        }
    }
}
