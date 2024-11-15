using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private Collider collider;

    public float corpseFlingForce = 1;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player (Remake)");

        Vector3 pushDir = transform.position - player.transform.position;

        rb.AddForce(pushDir * corpseFlingForce, ForceMode.Impulse);
        StartCoroutine(StopAllMoving());
    }

    IEnumerator StopAllMoving()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(rb);
        Destroy(collider);  
    }
}
