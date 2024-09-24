using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{
    private RaycastHit targetCheck;
    public GameObject holdingObject;
    public GameObject heldObjectPlace;
    public GameObject rockPrefab;

    public bool holdingCheck = false;

    private int grabMask;
    private int pickupMask;

    public float rockCount = 0;
    public float medKitCount = 0;
    public bool axe = true;

    // Start is called before the first frame update
    void Start()
    {
        grabMask = 1 << 6;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * 2, Color.red);

        //if you right click, then check if whatever you are looking at is grabbable or a pickup
        if (Input.GetMouseButtonDown(1))
        {
            TargetTesting();
        }

        if (holdingCheck)
        {
            holdingObject.transform.position = heldObjectPlace.transform.position;
        }

        //if holding object and you click, throw object
        if (holdingCheck && Input.GetMouseButtonDown(0))
        {
            holdingObject.GetComponent<Collider>().isTrigger = false;
            holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            holdingObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            holdingObject.GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse);
            holdingObject = null;
            holdingCheck = false;
        }

        //take rock out and hold it
        if (Input.GetKeyDown(KeyCode.R) && rockCount > 0 && !holdingCheck)
        {
            
            holdingObject = Instantiate(rockPrefab, heldObjectPlace.transform.position, heldObjectPlace.transform.rotation);
            holdingCheck = true;
            holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            holdingObject.GetComponent<Collider>().isTrigger = true;
            rockCount -= 1;
        }

        //heal if you have medkit
        if (Input.GetKeyDown(KeyCode.H) && medKitCount > 0)
        {
            Debug.Log("Used medkit!");
            GetComponentInParent<NewPlayerMovement>().health += 25;
            medKitCount -= 1;

            if (GetComponentInParent<NewPlayerMovement>().health > 100)
            {
                GetComponentInParent<NewPlayerMovement>().health = 100;
            }
        }
    }

    void TargetTesting()
    {
        if (Physics.Raycast(transform.position, transform.forward, out targetCheck, 5, grabMask))
        {
            if (targetCheck.transform.CompareTag("Grabbable"))
            {
                Debug.Log("Run grab event!");
                HoldObject();
            }
        }

        if (Physics.Raycast(transform.position, transform.forward, out targetCheck, 5, grabMask))
        {
            if (targetCheck.transform.CompareTag("Rock"))
            {
                rockCount += 1;

                Destroy(targetCheck.transform.gameObject);
                Debug.Log("Rock!");
            }
        }

        if (Physics.Raycast(transform.position, transform.forward, out targetCheck, 5, grabMask))
        {
            if (targetCheck.transform.CompareTag("Medkit"))
            {
                medKitCount += 1;

                Destroy(targetCheck.transform.gameObject);
                Debug.Log("Medkit!");
            }
        }

        if (Physics.Raycast(transform.position, transform.forward, out targetCheck, 5, grabMask))
        {
            if (targetCheck.transform.CompareTag("Axe"))
            {
                axe = true;
                GetComponentInParent<AxeSlash>().rightHand.SetActive(false);
                GetComponentInParent<AxeSlash>().axeSprite.SetActive(true);

                Destroy(targetCheck.transform.gameObject);
            }
        }
    }

    void HoldObject()
    {
        Debug.Log("Grabbable!");
        holdingObject = targetCheck.collider.gameObject;
        holdingCheck = true;
        holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        holdingObject.GetComponent<Collider>().isTrigger = true;
    }
}
