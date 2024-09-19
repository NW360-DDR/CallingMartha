using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSlash : MonoBehaviour
{
    public GameObject hitBox;
    public GameObject axeThrowPrefab;
    private GrabAndThrow grabScript;
    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        grabScript = GetComponentInChildren<GrabAndThrow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking && grabScript.axe)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject currentAxe = Instantiate(axeThrowPrefab, hitBox.transform.position, hitBox.transform.rotation);
            currentAxe.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
            grabScript.axe = false;
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.15f);
        hitBox.SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        hitBox.SetActive(false);
        isAttacking = false;
    }
}
