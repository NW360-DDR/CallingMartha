using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthAndRespawn : MonoBehaviour
{
    public int health = 3;

    public Vector3 checkpoint;
    public Slider healthSlider;
    public RawImage redJelly;

    public bool alive = true;
    private bool respawned = false;
    private bool hurtCool = false;
    private InventoryScript InventoryScript;
    private GrabAndThrow grabScript;
    private EclipseTimer timerScript;
    private bool healReset = false;
    private float healthHold = 0;
    private Rigidbody playerRB;
    private Animator blackScreen;

    private void Start()
    {
        InventoryScript = GetComponent<InventoryScript>();
        grabScript = GetComponentInChildren<GrabAndThrow>();
        redJelly = GameObject.Find("Red Overlay").GetComponent<RawImage>();
        timerScript = GameObject.Find("EclipseTimer").GetComponent<EclipseTimer>();
        blackScreen = GameObject.Find("Fade").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !respawned)
        {
            //kill player and run the respawn coroutine
            alive = false;
            GetComponent<CameraScript>().enabled = false;
            grabScript.enabled = false;
            GetComponent<AxeSlash>().enabled = false;
            GetComponent<NewPlayerMovement>().enabled = false;
            StartCoroutine(Respawn());
        }

        //heal if the player has a medkit and held down H for 1 second
        healthSlider.value = healthHold;
        if (Input.GetKey(KeyCode.H) && InventoryScript.medKitCount > 0 && health < 3 && !healReset)
        {
            healthHold += Time.deltaTime;

            if (healthHold >= 1)
            {
                Debug.Log("Used medkit!");
                redJelly.color = new Color(redJelly.color.r, redJelly.color.g, redJelly.color.b, 0f);
                health = 3;
                InventoryScript.medKitCount -= 1;
            }
        }
        else
        {
            healthHold = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //player can only be hurt when hurtcooldown is over, also reset the healing timer if player is healing
        if (other.CompareTag("Hurtbox") && !hurtCool)
        {
            health -= 1;
            redJelly.color += new Color (redJelly.color.r, redJelly.color.g, redJelly.color.b, 0.50f);
            hurtCool = true;
            healReset = true;
            StartCoroutine(HitCooldown());
        }

        if (other.CompareTag("Spawn Trigger"))
        {
            other.GetComponent<EnemySpawnTrigger>().SendMessageUpwards("SpawnEnemies");
        }

        if (other.CompareTag("Die"))
        {
            health = 0;
            redJelly.color += new Color(redJelly.color.r, redJelly.color.g, redJelly.color.b, 0.60f);
            respawned = true;
            alive = false;
            GetComponent<CameraScript>().enabled = false;
            grabScript.enabled = false;
            GetComponent<AxeSlash>().enabled = false;
            GetComponent<NewPlayerMovement>().enabled = false;
            gameObject.AddComponent<Rigidbody>();
            StartCoroutine(timerScript.Restart());
        }
    }

    IEnumerator Respawn()
    {
        //resets player location and health
        Debug.Log("Respawn?");
        playerRB = gameObject.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(1.5f);
        blackScreen.SetBool("FadeIn", true);
        respawned = true;
        redJelly.color = new Color(redJelly.color.r, redJelly.color.g, redJelly.color.b, 0f);
        transform.position = checkpoint;
        health = 3;
        alive = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(playerRB);
        GetComponent<CameraScript>().enabled = true;
        InventoryScript.enabled = true;
        GetComponent<AxeSlash>().enabled = true;
        GetComponent<NewPlayerMovement>().enabled = true;
        grabScript.enabled = true;
        respawned = false;
        blackScreen.SetBool("FadeIn", false);
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        healReset = false;
        hurtCool = false;
    }
}
