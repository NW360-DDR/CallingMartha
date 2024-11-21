using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrabAndThrow : MonoBehaviour
{
    private RaycastHit targetCheck;
    public GameObject holdingObject;
    public GameObject heldObjectPlace;
    public GameObject rockPrefab;
    private TextMeshProUGUI interactText;

    public bool holdingCheck = false;

    public bool canPickupAxe = true;

    public LayerMask excludeLayer;

    private Collider holdingObjectCollider;
    private Rigidbody holdingObjectRB;

    // Start is called before the first frame update
    void Start()
    {
        //arbitrary comment for nate
        interactText = GameObject.Find("Interact Text").GetComponent<TextMeshProUGUI>();
        interactText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!holdingCheck)
        TargetTesting();

        //checks if the player is holding an object. if they are, set the location to the held object place position
        if (holdingCheck)
        {
            holdingObjectRB.MovePosition(Vector3.Lerp(holdingObject.transform.position, heldObjectPlace.transform.position, Time.deltaTime * 10f));
            holdingObjectRB.MoveRotation(Quaternion.Lerp(holdingObject.transform.rotation, heldObjectPlace.transform.rotation, Time.deltaTime * 10f));
            holdingObjectRB.velocity = Vector3.zero;
            //holdingObject.transform.SetPositionAndRotation(heldObjectPlace.transform.position, heldObjectPlace.transform.rotation);
            //holdingObjectRB.MovePosition(heldObjectPlace.transform.position);
        }
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
                if (!holdingCheck)
                {
                    interactText.text = "E - Pick up";
                }else
                    interactText.text = "E - Put down";

                if (holdingCheck && Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0)
                {
                    GetComponentInParent<AxeSlash>().enabled = true;
                    GetComponentInParent<GunScript>().enabled = true;
                    //holdingObjectCollider.isTrigger = false;
                    holdingObjectRB.useGravity = true;
                    holdingObjectRB.constraints = RigidbodyConstraints.None;
                    holdingObjectRB.velocity = Vector3.zero;

                    holdingObjectRB = null;
                    holdingObjectCollider = null;
                    holdingObject = null;
                    holdingCheck = false;
                    interactText.gameObject.SetActive(false);
                } else if (Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0)
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
                if (Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0)
                {
                    targetCheck.transform.gameObject.SendMessageUpwards("Interact");
                    interactText.gameObject.SetActive(false);
                }
            }
            else
                interactText.gameObject.SetActive(false);
        } else
            interactText.gameObject.SetActive(false);

        //if holding object and you click, throw object
        /*if (holdingCheck && Input.GetMouseButtonDown(0))
        {
            GetComponentInParent<AxeSlash>().enabled = true;
            GetComponentInParent<GunScript>().enabled = true;
            holdingObjectCollider.isTrigger = false;
            holdingObjectRB.constraints = RigidbodyConstraints.None;
            holdingObjectRB.velocity = Vector3.zero;
            holdingObjectRB.AddForce(transform.forward * 15, ForceMode.Impulse);

            holdingObjectRB = null;
            holdingObjectCollider = null;
            holdingObject = null;
            holdingCheck = false;
            interactText.gameObject.SetActive(false);
        }*/
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
        holdingObjectRB.useGravity = false;
        //holdingObjectCollider.isTrigger = true;
        GetComponentInParent<AxeSlash>().enabled = false;
        GetComponentInParent<GunScript>().enabled = false;
    }
}