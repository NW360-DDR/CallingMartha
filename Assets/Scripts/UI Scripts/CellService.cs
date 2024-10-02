using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellService : MonoBehaviour
{
    [SerializeField]RectTransform serviceBars;
    public float serviceWidth;

    private void Start()
    {
        serviceBars = GetComponent<RawImage>().rectTransform;
        serviceWidth = transform.parent.gameObject.GetComponentInParent<RawImage>().rectTransform.sizeDelta.x;
        InvokeRepeating("Test", 1, 1);
    }
    public void ServiceUpdate(int level)
    {
        if (level < 0 || level > 5)// If it's not within range, something is wrong here.
        {
            Debug.Log("Yeah something is calling me and is doing it wrong.");
        }
        else
        {
            serviceBars.sizeDelta = new Vector2(serviceWidth / 5f * level, serviceBars.sizeDelta.y);
            Debug.Log(level + ", width = " + serviceBars.sizeDelta.x);
        }
    }
    public void Test()
    {
        ServiceUpdate(Random.Range(0, 6));
    }

}
