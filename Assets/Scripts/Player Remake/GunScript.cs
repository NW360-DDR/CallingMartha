using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject gun;

    private InventoryScript inventoryScript;

    private void Start()
    {
        inventoryScript = GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //its a gun. shoot it.
        if (Input.GetMouseButtonDown(1) && inventoryScript.bulletCount >= 1)
        {
            //gun.GetComponent<Animator>().SetTrigger("Shoot");

            inventoryScript.bulletCount -= 1;
        }
    }
}
