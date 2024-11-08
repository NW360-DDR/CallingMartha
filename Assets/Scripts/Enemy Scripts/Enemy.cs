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
        if (isAnimated)
        spriteAnim.SetFloat("Rotation", angleCalcScript.index);

        if (health <= 0)
        {
            Yelp.Play();
            StartCoroutine(Die());
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
            try
            {
                GenericEnemy brain = GetComponent<GenericEnemy>();
                if (!brain.brain.GetState().Equals("GetHit"))
                {
                    brain.DoDamage();
                    Debug.Log("Ouch!");
                    health -= 50;
                    Yelp.Play();
                }
            }
            catch (System.Exception)
            {

                MarthaTestScript brain = GetComponent<MarthaTestScript>();
                if (!brain.brain.GetState().Equals("GetHit"))
                {
                    Debug.Log("Ouch! Martha wtf");
                    health -= 50;
                    Yelp.Play();
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
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Oh no! Am Dead!");
        if(isMartha)
        SceneManager.LoadScene("End Scene");
        Destroy(gameObject);
    }
}
