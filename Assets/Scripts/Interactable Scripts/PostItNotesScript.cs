using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostItNotesScript : Interactable
{
    public int noteNum = 0;

    // Start is called before the first frame update
    void Interact()
    {
        if (noteNum == 0)
        {
            logString = "\"Station phones haven't been working too well. \n The watch tower should have cell service. \n I need to head over later.\" \n\n -Officer Ronan";
            SendLog();
        }

        else if (noteNum == 1)
        {
            logString = "This is another test.";
            SendLog();
        }
    }
}
