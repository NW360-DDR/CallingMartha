using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockThrowScript : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject spawnLocation;

    private GameObject thrownRock;
    private InventoryScript inventoryScript;

    private void Start()
    {
        inventoryScript = GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //its a rock. throw it.
        if (Input.GetMouseButtonDown(1) && inventoryScript.rockCount >= 1)
        {
            thrownRock = Instantiate(rockPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
            thrownRock.GetComponent<Rigidbody>().AddForce(spawnLocation.transform.forward * 30, ForceMode.Impulse);
            thrownRock = null;
            inventoryScript.rockCount -= 1;
        }
    }
}
