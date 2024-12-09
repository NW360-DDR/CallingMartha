using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepTryTheFourth : MonoBehaviour
{

    public GameObject Player;
    public CharacterController playerController;
    public float footstepSpeed;
    public float time = 0.0f;

    public bool isInside = false;
    public LayerMask floorLayer;

    public FMODUnity.EventReference woodFootsteps;
    public FMODUnity.EventReference dirtFootsteps;


    // Start is called before the first frame update
    void Start()
    {
        floorLayer = LayerMask.GetMask("WoodFloor");
    }

    // Update is called once per frame
    void Update()
    {
        GroundRayCast();
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

    public void GroundRayCast()
    {
        
        if (Physics.Raycast(Player.transform.position, Vector3.down, 4.0f, floorLayer))
        {
            isInside = true;
        }
        else
        {
            isInside = false;
        }
       
        
    }

    public void SelectAndPlayFootstep()
    {
        if (isInside == false)
        {
            Debug.Log("OUTSIDE");
            FMODUnity.RuntimeManager.PlayOneShotAttached(dirtFootsteps, Player);
        }

        if (isInside == true)
        {
            Debug.Log("INSIDE");
            FMODUnity.RuntimeManager.PlayOneShotAttached(woodFootsteps, Player);
        }
        
        Debug.Log("Should be playing something");
    }
}
