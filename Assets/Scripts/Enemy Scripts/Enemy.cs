using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public bool isAnimated = true;
    public bool isMartha = false;
    private bool dead = false;

    private Animator spriteAnim;
    private AngleCalc angleCalcScript;

    
    public AudioManager AudioManager;
    public GameObject deadWolf;
    // Start is called before the first frame update
    void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleCalcScript = GetComponent<AngleCalc>();
        AudioManager = GameObject.FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimated)
        spriteAnim.SetFloat("Rotation", angleCalcScript.index);

        if (health <= 0)
        {
            AudioManager.WolfHurt();
            StartCoroutine(Die());
        }
    }

    void GetShot()
    {
        AudioManager.WolfHurt();
        Debug.Log("I done got shot!");
        StartCoroutine(Die());
    }

    void ShootWife()
    {
        Debug.Log("Ouch! You Shot Your Wife!");
        health -= 100;
        AudioManager.WolfHurt();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AxeHitbox"))
        {
            if (!isMartha)
            {
                GenericEnemy brain = GetComponent<GenericEnemy>();
                if (!brain.brain.GetState().Equals("GetHit"))
                {
                    brain.DoDamage();
                    Debug.Log("Ouch!");
                    health -= 50;
                    AudioManager.WolfHurt();
                }
            }
            else
            {

                MarthaTestScript brain = GetComponent<MarthaTestScript>();
                if (!brain.brain.GetState().Equals("GetHit"))
                {
                    Debug.Log("Ouch! Martha wtf");
                    health -= 50;
                    AudioManager.WolfHurt();
                }
            }
            
            
            //GetComponent<NavMeshAgent>().speed = 0;
            //StartCoroutine(ResetSpeed());
            
        }
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<NavMeshAgent>().speed = 3.5f;
    }

    IEnumerator Die()
    {
        if (!isMartha && !dead)
        {
            dead = true;
            Instantiate(deadWolf, transform.position, transform.rotation);
        }

        yield return new WaitForSeconds(0.1f);
        Debug.Log("Oh no! Am Dead!");
        if(isMartha)
        SceneManager.LoadScene("End Scene");
        Destroy(gameObject);
    }
}
