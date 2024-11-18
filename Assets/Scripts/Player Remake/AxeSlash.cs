using UnityEditor;
using UnityEngine;

public class AxeSlash : MonoBehaviour
{
    public GameObject hitBox;
    public GameObject weapon;

    private EquippedScript equipScript;
    private Animator axeAnim;
    public bool attackSignal = false;

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
        }else
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
}
