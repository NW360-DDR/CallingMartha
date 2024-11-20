using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    private CellService cellService;

    public Vector3 moveDirection;
    private Vector3 velocity;
    public float speed = 5;
    public float dashSpeed;
    public float dashTime;
    public float airTime = 0;

    public bool isGrounded;

    public GameObject pauseMenu;
    private EclipseTimer eclipseScript;

    [SerializeField] HealthAndRespawn healthScript;

    private bool isDashing = false;
    private bool dashCooldown = false;

    private int grabMask;

    readonly private float gravity = -9.81f;
    public float fallDamageThreshold = -15f;

    private RaycastHit groundCheck;
    bool willDie = false;
    // Start is called before the first frame update
    void Start()
    {
        cellService = GameObject.Find("ServiceBar").GetComponent<CellService>();
        eclipseScript = GameObject.Find("EclipseTimer").GetComponent<EclipseTimer>();
        grabMask = 1 << 6;
        pauseMenu.SetActive(false);
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        

        //set the movement direction vector3 if the player is still alive
        if (healthScript.alive)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        }

        //actually move the player
        //characterController.Move((moveDirection * speed) * Time.deltaTime);
        moveDirection *= speed;

        //artificial gravity
        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            if (Mathf.Abs(velocity.y) >= Mathf.Abs(fallDamageThreshold))
            {
                willDie = true;
            }
            else
            {
                willDie = false;
            }
        }
        else
        {
            if (willDie)
            {
                healthScript.GetHurt(3);
            }
        }
        isGrounded = characterController.isGrounded;
        //let the player jump if they are grounded and not dashing
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded && !isDashing)
        {
            velocity.y = Mathf.Sqrt(3f * -gravity);
            GetComponentInChildren<Animator>().SetBool("isWalking", false);
        }
        moveDirection += velocity;
        
        characterController.Move((moveDirection) * Time.deltaTime);

        

        //allow the player to dash if they are grounded and not on cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && Grounded() && !dashCooldown)
        {
            dashCooldown = true;
            isDashing = true;
            speed *= dashSpeed;
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                eclipseScript.gameTimerActive = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public bool Grounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out groundCheck, 1.35f))
        {
            if (groundCheck.transform.CompareTag("Ground") || groundCheck.transform.CompareTag("Grabbable"))
            {
                return true;
            }
            else if (Physics.Raycast(transform.position, Vector3.down, out groundCheck, 1.2f, grabMask))
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cellbox"))
        {
            cellService.ServiceUpdate(other.GetComponent<CellVolume>().cellPower);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cellbox") && cellService.service < other.GetComponent<CellVolume>().cellPower)
        {
            cellService.ServiceUpdate(other.GetComponent<CellVolume>().cellPower);
            cellService.inCellBox = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cellbox"))
        {
            cellService.ServiceUpdate(0);
            cellService.inCellBox = false;
        }
    }
}