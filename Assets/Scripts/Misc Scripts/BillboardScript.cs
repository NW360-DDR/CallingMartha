using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public GameObject player;
    Vector3 lookDir;

    private void Start()
    {
        player = GameObject.Find("Player (Remaeke");
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        transform.LookAt(lookDir);
    }
}
