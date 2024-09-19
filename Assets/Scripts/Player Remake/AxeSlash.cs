using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSlash : MonoBehaviour
{
    public GameObject hitBox;
    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
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
