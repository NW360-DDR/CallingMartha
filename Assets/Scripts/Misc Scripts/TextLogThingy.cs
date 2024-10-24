using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLogThingy : MonoBehaviour
{

    GrabAndThrow rayCaster;
    public TextMeshProUGUI HUDText;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rayCaster = GameObject.Find("Camera").GetComponentInChildren<GrabAndThrow>();
        TextPush("Hi");
    }

    // Update is called once per frame
    void Update()
    {
        switch (rayCaster.tag)
        {
            default:
                break;
        }
    }

    public void TextPush(string text)
    {
        HUDText.text = text;
        HUDText.color = new Color(1, 1, 1, 1);
        anim.Play("Fadingtext", -1, 0.0f);
    }
}

