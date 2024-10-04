using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector3 velocity;
    public float speed = 5;
    public float dashSpeed;
    public float dashTime;

    private HealthAndRespawn healthScript;

    private bool isDashing = false;
    private bool dashCooldown = false;

    private int grabMask;

    readonly private float gravity = -9.81f;

    private RaycastHit groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        healthScript = GetComponent<HealthAndRespawn>();
        grabMask = 1 << 6;
    }

    // Update is called once per frame
    void Update()
    {
        // makes it so the player isn't building falling velocity while grounded
        if (Grounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //make animation play based on input from axis
        if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f)
        {
            GetComponentInChildren<Animator>().SetBool("isWalking", false);
        }else
        {
            if (Grounded())
            {
                GetComponentInChildren<Animator>().SetBool("isWalking", true);
            }
        }

        //set the movement direction vector3 if the player is still alive
        if (healthScript.alive)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        }

        //actually move the player
        characterController.Move((moveDirection * speed) * Time.deltaTime);

        //artificial gravity
        velocity.y += gravity * Time.deltaTime;

        //set velocity so the player will fall
        characterController.Move(velocity * Time.deltaTime);

        //let the player jump if they are grounded and not dashing
        if (Input.GetKeyDown(KeyCode.Space) && Grounded() && !isDashing)
        {
            velocity.y = Mathf.Sqrt(1.5f * -2f * gravity);
            GetComponentInChildren<Animator>().SetBool("isWalking", false);
        }

        //allow the player to dash if they are grounded and not on cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && Grounded() && !dashCooldown)
        {
            dashCooldown = true;
            isDashing = true;
            speed *= dashSpeed;
            StartCoroutine(Dash());
        }
    }

    private bool Grounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out groundCheck, 1.17f))
        {
            if (groundCheck.transform.CompareTag("Ground") || groundCheck.transform.CompareTag("Grabbable"))
            {
                return true;
            }
            else if (Physics.Raycast(transform.position, Vector3.down, out groundCheck, 1.17f, grabMask))
            {
                return true;
            }
            else
            {
                GetComponentInChildren<Animator>().SetBool("isWalking", false);
                return false;

            }
        }else
        {
            GetComponentInChildren<Animator>().Play("Camera_Idle");
            GetComponentInChildren<Animator>().SetBool("isWalking", false);
            return false;
        }
    }

    //this is run when the dash is used
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashTime);
        speed /= dashSpeed;
        isDashing = false;
        yield return new WaitForSeconds(1);
        dashCooldown = false;
    }
}