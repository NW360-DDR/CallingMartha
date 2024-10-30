using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class PlayerBobbing : MonoBehaviour
{
    public float verticalIntensity;
    public float horizontalIntensity;
    public float effectSpeed;
    public float resetSpeed;

    private Vector3 origPos;
    private float sinTime;
    public NewPlayerMovement playerMovement;

    private void Start()
    {
        origPos = transform.localPosition;
        playerMovement = GameObject.Find("Player (Remake)").GetComponent<NewPlayerMovement>();
    }

    private void Update()
    {
        Vector3 inputV = new(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        if (inputV.magnitude > 0f && playerMovement.Grounded())
        {
            sinTime += Time.deltaTime * effectSpeed;
            float sinY = verticalIntensity * Mathf.Sin(sinTime);
            Vector3 sinX =  verticalIntensity * -Mathf.Cos(sinTime/2) * horizontalIntensity * transform.right;
            transform.localPosition = new Vector3
            {
                x = transform.localPosition.x,
                y = origPos.y + sinY,
                z = transform.localPosition.z
            };
            transform.position += sinX;

        }
        else
        {
            sinTime = 0;
            StopHeadbob();
        }
    }

    private void StopHeadbob()
    {

        if (transform.localPosition == origPos) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, origPos, resetSpeed * Time.deltaTime);

    }

}
