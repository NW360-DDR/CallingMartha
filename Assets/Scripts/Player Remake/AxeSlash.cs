using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeSlash : MonoBehaviour
{
    public GameObject hitBox;
    public GameObject weapon;

    private InventoryScript inventoryScript;
    private Animator axeAnim;
    public bool attackSignal = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = GetComponent<InventoryScript>();
        axeAnim = weapon.GetComponent<Animator>();
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

        // if the player clicks, attack
        if (Input.GetMouseButtonDown(0))
        {
            axeAnim.SetTrigger("Attack");
        }

        // if the player is readying and releases the button, do the attack animation
        /*if (Input.GetMouseButtonUp(0) && takeInput && inventoryScript.axe)
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
        }*/
    }

    void TurnOnHitbox()
    {
        hitBox.SetActive(true);
    }

    void TurnOffHitbox()
    {
        hitBox.SetActive(false);
    }
}
