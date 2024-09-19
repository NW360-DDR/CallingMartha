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

    public float rockCount = 0;

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

        if (Input.GetKeyDown(KeyCode.R) && rockCount > 0 && !holdingCheck)
        {
            
            holdingObject = Instantiate(rockPrefab, heldObjectPlace.transform.position, heldObjectPlace.transform.rotation);
            holdingCheck = true;
            holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            rockCount -= 1;
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

        if (Physics.Raycast(transform.position, transform.forward, out targetCheck))
        {
            if (targetCheck.transform.CompareTag("Rock"))
            {
                rockCount += 1;

                Destroy(targetCheck.transform.gameObject);
                Debug.Log("Rock!");
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
