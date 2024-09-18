using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{
    private RaycastHit targetCheck;
    public GameObject holdingObject;
    public GameObject heldObjectPlace;

    public bool holdingCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            TargetTesting();
        }

        if (holdingCheck)
        {
            holdingObject.transform.position = heldObjectPlace.transform.position;
        }

        if (holdingCheck && Input.GetMouseButtonDown(0))
        {
            holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            holdingObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            holdingObject.GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse);
            holdingObject = null;
            holdingCheck = false;
        }
    }

    void TargetTesting()
    {
        if (Physics.Raycast(transform.position, transform.forward, out targetCheck))
        {
            if (targetCheck.transform.CompareTag("Grabbable"))
            {
                Debug.Log("Run grab event!");
                HoldObject();
            }else
            {
                Debug.Log("Nothing!");
            }
        }
    }

    void HoldObject()
    {
        Debug.Log("Grabbable!");
        holdingObject = targetCheck.collider.gameObject;
        holdingCheck = true;
        holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}
