using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDirection;
    private Rigidbody playerRB;
    private RaycastHit groundCheck;
    private RaycastHit slopeCheck;

    public float speed;
    public float speedMultiplier;
    public float maxSlope;
    public float slopeGrav;
    public float dashSpeed = 3;
    public float dashTime = 0.2f;
    public bool isDashing = false;

    float tempPlayerx;
    float tempPlayerz;

    public float ShowMeY;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Grounded())
            {
                playerRB.AddForce(transform.up * 7, ForceMode.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Grounded() && !OnSlope())
            {
                isDashing = true;
                speed = speed * dashSpeed;

                //playerRB.AddForce(moveDirection * 5, ForceMode.Impulse);
                //transform.position = Vector3.MoveTowards(transform.position, moveDirection * 5, speed * Time.deltaTime);
                StartCoroutine(Dash());
            }
        }
    }

    private void FixedUpdate()
    {
        //grab input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //store player y velocity for later use
        float y = playerRB.velocity.y;

        //calculate the move direction
        moveDirection = transform.forward * vertical + transform.right * horizontal;

        //actually move the player
        if (OnSlope())
        {
            if (isDashing)
            {
                playerRB.velocity = Vector3.zero;
                playerRB.angularVelocity = Vector3.zero;
            }
            playerRB.AddForce(GetSlopeMoveDir() * speed * speedMultiplier, ForceMode.Force);

            if (playerRB.velocity.y > 0)
            {
                playerRB.AddForce(Vector3.down * slopeGrav, ForceMode.Force);
                //playerRB.constraints = RigidbodyConstraints.FreezePositionY;
            }

            playerRB.useGravity = false;
        }
        else
        {
            playerRB.AddForce(moveDirection.normalized * speed * speedMultiplier, ForceMode.Force);
            playerRB.useGravity = true;
        }
        

        //slowly make the player come to a stop if there are no inputs
        if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f)
        {
            tempPlayerx = playerRB.velocity.x;
            tempPlayerz = playerRB.velocity.z;
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;

            playerRB.velocity = new Vector3(tempPlayerx, playerRB.velocity.y, tempPlayerz);
            playerRB.velocity = playerRB.velocity * 0.9f;
        }

        //clamp the player speed
        if (playerRB.velocity.magnitude > speed)
        {
            playerRB.velocity = playerRB.velocity.normalized * speed;
        }

        //use the stored y velocity to force normal gravity
        playerRB.velocity = new Vector3(playerRB.velocity.x, y, playerRB.velocity.z);
        ShowMeY = y;
    }

    private bool Grounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out groundCheck, 2))
        {
            if (groundCheck.transform.CompareTag("Ground"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeCheck, 2f))
        {
            Debug.Log("Slope!");
            float angle = Vector3.Angle(Vector3.up, slopeCheck.normal);

            //if there is a slope, but it's smaller than max slope
            return angle < maxSlope && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDir()
    {
        //calculate slope move direction

        /*Quaternion slopeAngleRotation = Quaternion.FromToRotation(playerRB.transform.up, slopeCheck.normal);

        Vector3 calculatedMovement = slopeAngleRotation * moveDirection;*/
        return Vector3.ProjectOnPlane(moveDirection, slopeCheck.normal).normalized;
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        speed = speed / dashSpeed;
    }
}
