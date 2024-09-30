using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellService : MonoBehaviour
{
    RectTransform serviceBars;
    float serviceWidth;

    private void Start()
    {
        serviceBars = GetComponent<RawImage>().rectTransform;
        serviceWidth = GetComponentInParent<RawImage>().rectTransform.rect.width;
    }
    public void ServiceUpdate(int level)
    {
        if ((level < 0 || level > 5))// If it's not within range, something is wrong here.
        {
            Debug.Log("Yeah something is calling me and is doing it wrong.");
        }
        else
        {
            serviceBars.sizeDelta = new Vector2(serviceWidth * level, serviceBars.sizeDelta.y);
        }
    }
    public void Test()
    {
        ServiceUpdate(Random.Range(0, 6));
    }
}
