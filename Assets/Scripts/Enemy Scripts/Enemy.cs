using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 100;

    private Animator spriteAnim;
    private AngleCalc angleCalcScript;

    public AudioSource Yelp;
    // Start is called before the first frame update
    void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleCalcScript = GetComponent<AngleCalc>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteAnim.SetFloat("Rotation", angleCalcScript.index);

        if (health <= 0)
        {
            Yelp.Play();
            Destroy(gameObject);
        }
    }

    void GetShot()
    {
        Yelp.Play();
        Debug.Log("I done got shot!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AxeHitbox"))
        {
            Debug.Log("Ouch!");
            health -= 50;
            Yelp.Play();
            GetComponent<NavMeshAgent>().speed = 0;
            StartCoroutine(ResetSpeed());
        }
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<NavMeshAgent>().speed = 3.5f;
    }
}
