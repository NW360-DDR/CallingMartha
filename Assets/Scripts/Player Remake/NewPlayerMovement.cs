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

    private float gravity = -9.81f;

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
        //Vector3 movedir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Grounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

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

        if (healthScript.alive)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        }

        characterController.Move(Time.deltaTime * speed * moveDirection);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && Grounded() && !isDashing)
        {
            velocity.y = Mathf.Sqrt(2 * -2f * gravity);
            GetComponentInChildren<Animator>().SetBool("isWalking", false);
        }

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

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashTime);
        speed /= dashSpeed;
        isDashing = false;
        yield return new WaitForSeconds(1);
        dashCooldown = false;
    }
}