using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellService : MonoBehaviour
{
    [SerializeField]RectTransform serviceBars;
    public float serviceWidth;
    public bool inCellBox = false;
    public byte service;


    private void Start()
    {
        serviceBars = GetComponent<RawImage>().rectTransform;
        serviceWidth = transform.parent.gameObject.GetComponentInParent<RawImage>().rectTransform.sizeDelta.x;
        ServiceUpdate(1);
    }
    public void ServiceUpdate(int level)
    {
        if (level < 0 || level > 3)// If it's not within range, something is wrong here.
        {
            Debug.Log("Yeah something is calling me and is doing it wrong.");
        }
        else
        {
            service = (byte)level;
            serviceBars.sizeDelta = new Vector2(serviceWidth / 3f * level, serviceBars.sizeDelta.y);
            Debug.Log(level + ", width = " + serviceBars.sizeDelta.x);
        }

    }
}
