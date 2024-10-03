using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Camera playerCam;
    Vector3 lookDir;

    private void Start()
    {
        playerCam = GameObject.Find("Player (Remake)").GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        lookDir = playerCam.transform.forward;
        lookDir.y = 0;

        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}
