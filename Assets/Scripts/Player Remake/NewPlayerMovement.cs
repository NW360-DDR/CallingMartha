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

    private bool isDashing = false;

    private float gravity = -9.81f;

    private RaycastHit groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 movedir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Grounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

        characterController.Move(moveDirection * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && Grounded() && !isDashing)
        {
            velocity.y = Mathf.Sqrt(2 * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Grounded())
        {
            isDashing = true;
            speed = speed * dashSpeed;
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

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashTime);
        speed = speed / dashSpeed;
        isDashing = false;
    }
}
