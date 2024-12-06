using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : Interactable
{
    [SerializeField] bool destroyOnTriggerEnter;
    [SerializeField] string tagFilter;
    [SerializeField] UnityEvent onTriggerEnter; // optional on trigger enter or exit, only using this script for text when player enters an area
    [SerializeField] UnityEvent onTriggerExit;  // could be useful for jumpscares tho

    [TextArea(3, 10)] //makes textarea in inspector bigger
    public string noteText;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
        
        logString = noteText;
        SendLog();

        onTriggerEnter.Invoke();

        if (destroyOnTriggerEnter)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
        onTriggerExit.Invoke();
    }
}
