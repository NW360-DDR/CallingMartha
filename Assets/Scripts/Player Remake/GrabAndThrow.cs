using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrabAndThrow : MonoBehaviour
{
    private RaycastHit targetCheck;
    private InventoryScript inventoryScript;
    public GameObject holdingObject;
    public GameObject heldObjectPlace;
    public GameObject rockPrefab;
    private TextMeshProUGUI interactText;

    private GameObject leftHandSprite;
    private GameObject axeSprite;

    public bool holdingCheck = false;

    public bool canPickupAxe = true;

    public LayerMask excludeLayer;

    private Collider holdingObjectCollider;
    private Rigidbody holdingObjectRB;


    // Start is called before the first frame update
    void Start()
    {
        leftHandSprite = GameObject.Find("Lefthand");
        axeSprite = GameObject.Find("Axe");

        inventoryScript = GetComponentInParent<InventoryScript>();
        interactText = GameObject.Find("Interact Text").GetComponent<TextMeshProUGUI>();
        interactText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!holdingCheck)
        TargetTesting();
    }

    // Update is called once per frame
    void Update()
    {
        //this large chunk of code is checking for what the player is looking at, then displaying text accordingly
        if (targetCheck.transform != null)
        {
            if (targetCheck.transform.CompareTag("Grabbable"))
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "E - Pickup";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactText.gameObject.SetActive(false);
                    Debug.Log("Run grab event!");
                    HoldObject();
                }
            }
            else if (targetCheck.transform.CompareTag("Interactable"))
            {
                interactText.text = "E - Interact";
                interactText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    targetCheck.transform.gameObject.SendMessageUpwards("Interact");
                    interactText.gameObject.SetActive(false);
                }
            }
            else
                interactText.gameObject.SetActive(false);
        } else
            interactText.gameObject.SetActive(false);

        //checks if the player is holding an object. if they are, set the location to the held object place position
        if (holdingCheck)
        {
            holdingObject.transform.SetPositionAndRotation(heldObjectPlace.transform.position, heldObjectPlace.transform.rotation);
            interactText.gameObject.SetActive(false);
        }

        //if holding object and you click, throw object
        if (holdingCheck && Input.GetMouseButtonDown(0))
        {
            axeSprite.GetComponent<Image>().enabled = true;
            leftHandSprite.GetComponent<Image>().enabled = true;
            GetComponentInParent<AxeSlash>().enabled = true;
            holdingObjectCollider.isTrigger = false;
            holdingObjectRB.constraints = RigidbodyConstraints.None;
            holdingObjectRB.velocity = Vector3.zero;
            holdingObjectRB.AddForce(transform.forward * 15, ForceMode.Impulse);

            holdingObjectRB = null;
            holdingObjectCollider = null;
            holdingObject = null;
            holdingCheck = false;
            interactText.gameObject.SetActive(false);
        }
        else if (holdingCheck && Input.GetMouseButton(1))
        {
            axeSprite.GetComponent<Image>().enabled = true;
            leftHandSprite.GetComponent<Image>().enabled = true;
            GetComponentInParent<AxeSlash>().enabled = true;
            holdingObjectCollider.isTrigger = false;
            holdingObjectRB.constraints = RigidbodyConstraints.None;
            holdingObjectRB.velocity = Vector3.zero;

            holdingObjectRB = null;
            holdingObjectCollider = null;
            holdingObject = null;
            holdingCheck = false;
            interactText.gameObject.SetActive(false);
        }
    }

    void TargetTesting()
    {
        //the actual raycast
        Physics.Raycast(transform.position, transform.forward, out targetCheck, 5, ~excludeLayer);
    }

    void HoldObject()
    {
        //runs when raycast found grabbable and player pressed E
        Debug.Log("Grabbable!");
        holdingObject = targetCheck.collider.gameObject;
        holdingCheck = true;

        holdingObjectCollider = holdingObject.GetComponent<Collider>();
        holdingObjectRB = holdingObject.GetComponent<Rigidbody>();

        holdingObjectRB.constraints = RigidbodyConstraints.FreezeRotation;
        holdingObjectCollider.isTrigger = true;
        axeSprite.GetComponent<Image>().enabled = false;
        leftHandSprite.GetComponent<Image>().enabled = false;
        GetComponentInParent<AxeSlash>().enabled = false;
    }
}