using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject gun;

    private InventoryScript inventoryScript;
    private EquippedScript equipScript;

    private bool gunCooldown = false;
    public LayerMask excludeLayer;

    private RaycastHit hit;

    public AudioManager AudioManager;
    private void Start()
    {
        inventoryScript = GetComponent<InventoryScript>();
        equipScript = GetComponent<EquippedScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(spawnLocation.transform.position, spawnLocation.transform.forward, Color.red);
    }

    public void Shoot()
    {
        Debug.Log("Shoot gun!");

        //its a gun. shoot it.
        if (Input.GetMouseButtonDown(0) && inventoryScript.bulletCount >= 1 && Time.timeScale > 0 && equipScript.allowAttack && !gunCooldown)
        {
            gun.GetComponent<Animator>().SetTrigger("Shoot");

            Physics.Raycast(spawnLocation.transform.position, spawnLocation.transform.forward, out hit, 30, ~excludeLayer);

            AudioManager.PlayGunFire();

            if (hit.collider != null)
            {
                string tag = hit.transform.tag;
                if (tag.Equals("Wolf"))
                {
                    hit.transform.SendMessageUpwards("GetShot");
                }
                else if (tag.Equals("Shootable"))
                    Destroy(hit.transform.gameObject);
                else if (tag.Equals("Martha"))
                    hit.transform.SendMessageUpwards("ShootWife");
            }

            inventoryScript.bulletCount -= 1;

            gunCooldown = true;
            StartCoroutine(GunCooldownTimer());
        }
    }

    IEnumerator GunCooldownTimer()
    {
        yield return new WaitForSeconds(0.5f);
        gunCooldown = false;
    }
}
