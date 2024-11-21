using UnityEditor;
using UnityEngine;

public class AxeSlash : MonoBehaviour
{
    public GameObject hitBox;
    public GameObject weapon;

    private EquippedScript equipScript;
    public Camera cam;
    private RaycastHit checkForBreakable;
    private Animator axeAnim;
    public bool attackSignal = false;
    public LayerMask excludeLayer;

    // Start is called before the first frame update
    void Start()
    {
        equipScript = GetComponent<EquippedScript>();
        axeAnim = weapon.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //recieve signal from animation script
        if (attackSignal)
        {
            TurnOnHitbox();
            BreakableCheck();
        }
        else
        {
            TurnOffHitbox();
        }

        // if the player clicks, attack
        if (Input.GetMouseButtonDown(0) && equipScript.allowAttack && Time.timeScale > 0)
        {
            axeAnim.SetTrigger("Attack");
        }
    }

    void TurnOnHitbox()
    {
        hitBox.SetActive(true);
    }

    void TurnOffHitbox()
    {
        hitBox.SetActive(false);
    }

    void BreakableCheck()
    {
        Physics.Raycast(cam.transform.position, cam.transform.forward, out checkForBreakable, 5, ~excludeLayer);

        if (checkForBreakable.transform != null)
        {
            if (checkForBreakable.transform.CompareTag("Breakable"))
            {
                Debug.Log("Can be broken!");

                Destroy(checkForBreakable.transform.gameObject);
            }
        }
    }
}
