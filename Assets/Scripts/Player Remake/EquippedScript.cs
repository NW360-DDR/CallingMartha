using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedScript : MonoBehaviour
{
    public GameObject weapon;
    private Animator weaponAnim;

    private InventoryScript inventoryScript;
    private GunScript gunScript;
    private AxeSlash axeScript;

    public int currentEquipped = 0;
    // Start is called before the first frame update
    void Start()
    {
        weaponAnim = weapon.GetComponent<Animator>();
        inventoryScript = GetComponent<InventoryScript>();
        gunScript = GetComponent<GunScript>();
        axeScript = GetComponent<AxeSlash>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentEquipped != 0)
        {
            currentEquipped = 0;
            gunScript.enabled = false;
            weaponAnim.SetTrigger("ToAxe");
            weaponAnim.SetBool("SwitchingHand", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentEquipped != 1 && inventoryScript.bulletCount > 0)
        {
            currentEquipped = 1;
            gunScript.enabled = true;
            weaponAnim.SetTrigger("ToGun");
            weaponAnim.SetBool("SwitchingHand", true);
        }
    }
}