using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostItNotesScript : Interactable
{
    [TextArea(3,10)] //makes textarea in inspector bigger
    public string noteText;

    // Start is called before the first frame update
    void Interact()
    {
        // Public string (from editor) that gets sent to log, then outputs the text

        logString = noteText;
        SendLog();
    }
}