using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabAndThrow : MonoBehaviour
{
    private RaycastHit targetCheck;
    private InventoryScript inventoryScript;
    public GameObject holdingObject;
    public GameObject heldObjectPlace;
    public GameObject rockPrefab;

    private GameObject leftHandSprite;
    private GameObject axeSprite;

    public bool holdingCheck = false;

    public bool canPickupAxe = true;

    private int grabMask;

    private Collider holdingObjectCollider;
    private Rigidbody holdingObjectRB;

    // Start is called before the first frame update
    void Start()
    {
        grabMask = 1 << 6;

        leftHandSprite = GameObject.Find("Lefthand");
        axeSprite = GameObject.Find("Axe");

        inventoryScript = GetComponentInParent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //if you right click, then check if whatever you are looking at is grabbable or a pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            TargetTesting();
        }

        //checks if the player is holding an object. if they are, set the location to the held object place position
        if (holdingCheck)
        {
            holdingObject.transform.SetPositionAndRotation(heldObjectPlace.transform.position, heldObjectPlace.transform.rotation);
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
        }
    }

    void TargetTesting()
    {
        //checking for whatever possible tags could be interacted with
        //this is incredibly ineffecient and will be changed later
        if (Physics.Raycast(transform.position, transform.forward, out targetCheck, 5, grabMask))
        {
            if (targetCheck.transform.CompareTag("Grabbable"))
            {
                Debug.Log("Run grab event!");
                HoldObject();
            }
            else if (targetCheck.transform.CompareTag("Interactable"))
            {
                targetCheck.transform.gameObject.SendMessageUpwards("Interact");
            }
            else if (targetCheck.transform.CompareTag("Axe"))
            {
                if (canPickupAxe)
                {

                    inventoryScript = GameObject.Find("Player (Remake)").GetComponent<InventoryScript>();

                    inventoryScript.axe = true;

                    GameObject.Find("Player (Remake)").GetComponentInParent<AxeSlash>().rightHand.SetActive(false);
                    GameObject.Find("Player (Remake)").GetComponentInParent<AxeSlash>().axeSprite.SetActive(true);

                    Destroy(targetCheck.transform.gameObject);
                }
            }
        }

        void HoldObject()
        {
            //runs when the raycast finds a grabbable object
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
}
