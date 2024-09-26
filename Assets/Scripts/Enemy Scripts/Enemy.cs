using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AxeHitbox"))
        {
            Debug.Log("Ouch!");
            health -= 50;
            GetComponent<NavMeshAgent>().speed = 0;
            StartCoroutine(ResetSpeed());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            if (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.5f)
            {
                Debug.Log("Ouch!");
                Destroy(other.gameObject);
                health -= 50;
                GetComponent<NavMeshAgent>().speed = 0;
                StartCoroutine(ResetSpeed());
            }
        }

        if (other.gameObject.CompareTag("Axe"))
        {
            if (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.5f)
            {
                Debug.Log("Ouch!");
                health -= 100;
                GetComponent<NavMeshAgent>().speed = 0;
                StartCoroutine(ResetSpeed());
            }
        }
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<NavMeshAgent>().speed = 3.5f;
    }
}
