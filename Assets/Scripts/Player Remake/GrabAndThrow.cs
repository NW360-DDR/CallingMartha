using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabAndThrow : MonoBehaviour
{
    private RaycastHit targetCheck;
    public GameObject holdingObject;
    public GameObject heldObjectPlace;
    public GameObject rockPrefab;

    private GameObject leftHandSprite;
    private GameObject axeSprite;

    public bool holdingCheck = false;

    public bool canPickupAxe = true;

    private int grabMask;

    public float healthHold = 0;
    public bool healReset = false;

    private HealthAndRespawn healthScript;
    private FlashlightScript lightScript;

    private Collider holdingObjectCollider;
    private Rigidbody holdingObjectRB;

    public int rockCount = 0;
    public int medKitCount = 0;
    public int flashLightBatteries = 0;
    public int generatorItems = 0;
    public bool axe = true;

    // Start is called before the first frame update
    void Start()
    {
        grabMask = 1 << 6;
        healthScript = GetComponentInParent<HealthAndRespawn>();
        lightScript = GetComponentInParent<FlashlightScript>();

        leftHandSprite = GameObject.Find("Lefthand");
        axeSprite = GameObject.Find("Axe");
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
        }else if (holdingCheck && Input.GetMouseButton(1))
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

        //heal if the player has a medkit and held down H for 1 second
        if (Input.GetKey(KeyCode.H) && medKitCount > 0 && healthScript.health < 3 && !healReset)
        {
            healthHold += Time.deltaTime;

            if (healthHold >= 1)
            {
                Debug.Log("Used medkit!");
                healthScript.health = 3;
                medKitCount -= 1;
            }
        }else
        {
            healthHold = 0;
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
            } else if (targetCheck.transform.CompareTag("Rock"))
            {
                rockCount += 1;

                Destroy(targetCheck.transform.gameObject);
                Debug.Log("Rock!");
            } else if (targetCheck.transform.CompareTag("Medkit"))
            {
                medKitCount += 1;

                Destroy(targetCheck.transform.gameObject);
                Debug.Log("Medkit!");
            } else if (targetCheck.transform.CompareTag("Axe") && canPickupAxe)
            {
                axe = true;
                GetComponentInParent<AxeSlash>().rightHand.SetActive(false);
                GetComponentInParent<AxeSlash>().axeSprite.SetActive(true);

                Destroy(targetCheck.transform.gameObject);
            } else if (targetCheck.transform.CompareTag("Checkpoint"))
            {
                Debug.Log("Updated Checkpoint!");
                healthScript.checkpoint = targetCheck.transform.gameObject;
            } else if (targetCheck.transform.CompareTag("Battery"))
            {
                if (flashLightBatteries <= 0)
                {
                    lightScript.batteryLife = 100;
                    lightScript.updatedBatteries = false;
                    lightScript.flickered = false;
                }

                flashLightBatteries += 1;

                Destroy(targetCheck.transform.gameObject);
            } else if (targetCheck.transform.CompareTag("Generator"))
            {
                targetCheck.transform.gameObject.SendMessageUpwards("GeneratorCheck");
            } else if (targetCheck.transform.CompareTag("Generator Item"))
            {
                generatorItems += 1;
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
