using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject gun;

    private InventoryScript inventoryScript;
    public LayerMask excludeLayer;

    private RaycastHit hit;

    private void Start()
    {
        inventoryScript = GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(spawnLocation.transform.position, spawnLocation.transform.forward, Color.red);
        //its a gun. shoot it.
        if (Input.GetMouseButtonDown(1) && inventoryScript.bulletCount >= 1)
        {
            gun.GetComponent<Animator>().SetTrigger("Shoot");

            Physics.Raycast(spawnLocation.transform.position, spawnLocation.transform.forward, out hit, 15, ~excludeLayer);

            if (hit.collider != null)
            {
                if (hit.transform.CompareTag("Wolf"))
                {
                    hit.transform.SendMessageUpwards("GetShot");
                }
                else if (hit.transform.CompareTag("Shootable"))
                    Destroy(hit.transform.gameObject);
            }

            inventoryScript.bulletCount -= 1;
        }
    }
}
