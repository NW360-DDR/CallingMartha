using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

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
    private bool playedSwoosh = false;

    public AudioManager AudioManager;

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
    }

    void TurnOnHitbox()
    {
        hitBox.SetActive(true);
        if (!playedSwoosh)
        {
            AudioManager.PlayAxeWhoosh();
            playedSwoosh = true;
        }
    }

    void TurnOffHitbox()
    {
        hitBox.SetActive(false);
        playedSwoosh = false;
    }

    void BreakableCheck()
    {
        Physics.Raycast(cam.transform.position, cam.transform.forward, out checkForBreakable, 5, ~excludeLayer);

        if (checkForBreakable.transform != null)
        {
            Debug.Log(checkForBreakable.transform.name);
            if (checkForBreakable.transform.CompareTag("Breakable"))
            {
                Debug.Log("Can be broken!");
                AudioManager.PlayAxeImpactWood();
                Destroy(checkForBreakable.transform.gameObject);
            }
        }
    }

    public void Attack()
    {
        Debug.Log("Axe attack!!!");

        if (Input.GetMouseButtonDown(0) && equipScript.allowAttack && Time.timeScale > 0)
        {
            axeAnim.Play("Axe_Attack_Rework");
        }
    }
}
