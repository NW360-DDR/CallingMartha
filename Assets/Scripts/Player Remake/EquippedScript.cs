using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedScript : MonoBehaviour
{
    public GameObject weapon;
    public GameObject phone;
    private Animator weaponAnim;
    private Animator phoneAnim;

    private InventoryScript inventoryScript;
    private GunScript gunScript;
    private AxeSlash axeScript;

    public int currentEquipped = 0;
    // Start is called before the first frame update
    void Start()
    {
        weaponAnim = weapon.GetComponent<Animator>();
        phoneAnim = phone.GetComponent<Animator>();
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
            axeScript.enabled = true;
            weaponAnim.SetTrigger("ToAxe");
            weaponAnim.SetBool("SwitchingHand", true);
            phoneAnim.SetBool("PhoneOpen", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentEquipped != 1 && inventoryScript.bulletCount > 0)
        {
            currentEquipped = 1;
            gunScript.enabled = true;
            axeScript.enabled = false;
            weaponAnim.SetTrigger("ToGun");
            weaponAnim.SetBool("SwitchingHand", true);
            phoneAnim.SetBool("PhoneOpen", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && currentEquipped !=2)
        {
            currentEquipped = 2;
            gunScript.enabled = false;
            axeScript.enabled = false;
            weaponAnim.SetTrigger("ToPhone");
            weaponAnim.SetBool("SwitchingHand", true);
            phoneAnim.SetBool("PhoneOpen", true);
        }
    }
}