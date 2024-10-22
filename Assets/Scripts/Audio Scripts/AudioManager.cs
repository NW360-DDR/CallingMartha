using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public bool AudioPlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (AudioPlaying == true)
        {
            Debug.Log("AUDIO MANAGER SEES YOU!!!");
        }
    }

   
}
