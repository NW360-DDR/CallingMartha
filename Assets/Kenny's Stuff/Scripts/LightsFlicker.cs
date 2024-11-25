using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsFlicker : MonoBehaviour
{
    public float flickerSpeed = 0.3f;
    private int randomizer = 0;
    public GameObject turnLightsOn;
    public GameObject turnLightsOff;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(true)
        {
            if (randomizer == 0)
            {
                    turnLightsOn.SetActive(false); // Turns lights off
            }
            else
            {
                    turnLightsOn.SetActive(true); // Keeps lights on
            }
            randomizer = Random.Range(0, 5);
            yield return new WaitForSeconds(flickerSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
