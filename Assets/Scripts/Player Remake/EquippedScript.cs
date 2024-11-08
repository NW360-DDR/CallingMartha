using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedScript : MonoBehaviour
{
    public GameObject weapon;
    public GameObject phone;
    public GameObject flashlightLight;
    private Animator weaponAnim;
    private Animator phoneAnim;

    private InventoryScript inventoryScript;
    private GunScript gunScript;
    private AxeSlash axeScript;
    private FlashlightScript flashlightScript;
    private PhoneHandler phoneHandler;

    public int currentEquipped = 0;
    public float batteryDrainMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        weaponAnim = weapon.GetComponent<Animator>();
        phoneAnim = phone.GetComponent<Animator>();
        inventoryScript = GetComponent<InventoryScript>();
        gunScript = GetComponent<GunScript>();
        axeScript = GetComponent<AxeSlash>();
        flashlightScript = GetComponent<FlashlightScript>();
        phoneHandler = GameObject.Find("PhoneCanvas").GetComponent<PhoneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0 && inventoryScript.bulletCount <= 0 && currentEquipped != 0)
        {
            currentEquipped = 0;
            StartCoroutine(SendUpdate());
        }
        else if (Input.mouseScrollDelta.y > 0 && currentEquipped != 0)
        {
            currentEquipped--;
            StartCoroutine(SendUpdate());
        }
        else if (Input.mouseScrollDelta.y < 0 && inventoryScript.bulletCount <= 0 && currentEquipped != 2)
        {
            currentEquipped = 2;
            StartCoroutine(SendUpdate());
        }
        else if (Input.mouseScrollDelta.y < 0 && currentEquipped != 2)
        {
            currentEquipped++;
            StartCoroutine(SendUpdate());
        }

        if (currentEquipped == 2 && phoneHandler.phoneBatteryLife > 0)
        {
            phoneHandler.phoneBatteryLife -= Time.deltaTime * batteryDrainMultiplier;
        }
    }

    IEnumerator SendUpdate()
    {
        //buffer switching so they don't conflict
        yield return new WaitForSeconds(0.09f);
        UpdateEquipped();
    }

    void UpdateEquipped()
    {
        if (currentEquipped == 0)
        {
            currentEquipped = 0;
            gunScript.enabled = false;
            flashlightScript.flashlightOn = false;
            flashlightScript.enabled = false;
            axeScript.enabled = true;
            weaponAnim.SetTrigger("ToAxe");
            weaponAnim.SetBool("SwitchingHand", true);
            phoneAnim.SetBool("PhoneOpen", false);
            flashlightLight.SetActive(false);
        }

        if (currentEquipped == 1 && inventoryScript.bulletCount > 0)
        {
            currentEquipped = 1;
            gunScript.enabled = true;
            flashlightScript.flashlightOn = false;
            flashlightScript.enabled = false;
            axeScript.enabled = false;
            weaponAnim.SetTrigger("ToGun");
            weaponAnim.SetBool("SwitchingHand", true);
            phoneAnim.SetBool("PhoneOpen", false);
            flashlightLight.SetActive(false);
        }

        if (currentEquipped == 2)
        {
            currentEquipped = 2;
            gunScript.enabled = false;
            axeScript.enabled = false;
            flashlightScript.enabled = true;
            weaponAnim.SetTrigger("ToPhone");
            weaponAnim.SetBool("SwitchingHand", true);
            phoneAnim.SetBool("PhoneOpen", true);
            flashlightLight.SetActive(false);
        }
    }
}