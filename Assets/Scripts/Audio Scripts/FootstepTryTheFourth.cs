using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepTryTheFourth : MonoBehaviour
{

    public GameObject Player;
    public CharacterController playerController;
    public float footstepSpeed;
    public float time = 0.0f;

    public FMODUnity.EventReference woodFootsteps;
    public FMODUnity.EventReference dirtFootsteps;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isGrounded && playerController.velocity.magnitude > 0)
        {
            Debug.Log("LOOK MA, I'M WALKING!!");
            if (time >= footstepSpeed)
            {
                SelectAndPlayFootstep();
                time = 0.0f;
                Debug.Log("STEP!! STEP!! STEP!!");
            }

            time += Time.deltaTime;
        }
    }

    public void SelectAndPlayFootstep()
    {
        Debug.Log("Should be playing something");
    }
}
