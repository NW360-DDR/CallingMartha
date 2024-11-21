using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLogThingy : MonoBehaviour
{

    public TextMeshProUGUI HUDText;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        TextPush("Press Escape to pause game");
    }
    public void TextPush(string text)
    {
        HUDText.text = text;
        HUDText.color = new Color(1, 1, 1, 1);
        anim.Play("Fadingtext", -1, 0.0f);
    }
}

