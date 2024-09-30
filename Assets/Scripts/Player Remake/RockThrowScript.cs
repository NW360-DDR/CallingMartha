using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockThrowScript : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject spawnLocation;

    private GameObject thrownRock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && GetComponentInChildren<GrabAndThrow>().rockCount >= 1)
        {
            thrownRock = Instantiate(rockPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
            thrownRock.GetComponent<Rigidbody>().AddForce(spawnLocation.transform.forward * 15, ForceMode.Impulse);
            thrownRock = null;
            GetComponentInChildren<GrabAndThrow>().rockCount -= 1;
        }
    }
}
