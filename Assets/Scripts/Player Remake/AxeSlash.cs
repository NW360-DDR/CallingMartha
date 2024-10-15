using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeSlash : MonoBehaviour
{
    public GameObject hitBox;
    public GameObject axeThrowPrefab;
    public GameObject axeSprite;
    public GameObject rightHand;
    public GameObject cam;

    private InventoryScript inventoryScript;
    private GrabAndThrow grabScript;
    public bool attackSignal = false;
    public bool takeInput = true;

    private bool axeCooldownBool = true;

    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = GetComponent<InventoryScript>();
        grabScript = GetComponentInChildren<GrabAndThrow>();
        axeSprite.GetComponent<Animator>().Play("Axe_Idle");
    }

    // Update is called once per frame
    void Update()
    {
        //recieve signal from animation script
        if (attackSignal)
        {
            TurnOnHitbox();
        }else
        {
            TurnOffHitbox();
        }

        // if the player is readying and releases the button, do the attack animation
        if (Input.GetMouseButtonUp(0) && takeInput && inventoryScript.axe)
        {
            if (axeSprite.GetComponent<Animator>().GetBool("HoldingDown"))
            {
                axeSprite.GetComponent<Animator>().SetTrigger("IsAttacking");
                takeInput = false;
            }
            
            axeSprite.GetComponent<Animator>().SetBool("HoldingDown", false);
        }

        // if the player can ready an attack, do so
        if (Input.GetMouseButtonDown(0) && takeInput && inventoryScript.axe)
        {
            axeSprite.GetComponent<Animator>().SetBool("HoldingDown", true);
        }

        // if the player has an axe, throw the axe
        if (Input.GetKeyDown(KeyCode.Q) && takeInput && inventoryScript.axe && axeCooldownBool)
        {
            axeSprite.SetActive(false);
            rightHand.SetActive(true);
            GameObject currentAxe = Instantiate(axeThrowPrefab, hitBox.transform.position, hitBox.transform.rotation);
            currentAxe.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 20, ForceMode.Impulse);
            inventoryScript.axe = false;
            grabScript.canPickupAxe = false;
            axeCooldownBool = false;
            StartCoroutine(AxeThrowCooldown());
        }
    }

    void TurnOnHitbox()
    {
        hitBox.SetActive(true);
    }

    void TurnOffHitbox()
    {
        hitBox.SetActive(false);
    }

    IEnumerator AxeThrowCooldown()
    {
        yield return new WaitForSeconds(1);

        axeCooldownBool = true;
        grabScript.canPickupAxe = true;
    }
}
