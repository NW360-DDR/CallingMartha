using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepRedo : MonoBehaviour
{
    private enum CURRENT_TERRAIN { WOOD = 0, GROUND = 1 };

    [SerializeField] private CURRENT_TERRAIN currentTerrain;

    [SerializeField] FMODUnity.EventReference footstepEvent;
    [SerializeField] FMODUnity.EventReference footstepWoodEvent;

    [SerializeField] float footstepSpeed = 0.3f;

    [SerializeField] GameObject Player;

    [SerializeField] CharacterController playerController;

    float time = 0.0f;

    public LayerMask excludeLayer;

    public void PlayFootstep(int terrain)
    {
       if (terrain == 0)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(footstepWoodEvent, Player);
            Debug.Log("Footsteps on Wooden Floor");
        }

       if (terrain == 1)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(footstepEvent, Player);
            Debug.Log("Footsteps on Muddy Ground");
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (playerController.velocity.magnitude > 0 && playerController.isGrounded)
        {
            if (time >= footstepSpeed)
            {
                SelectAndPlayFootstep();
                time = 0.0f;
                Debug.Log("STEP!! STEP!! STEP!!");
            }

            time += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        DetermineTerrain();
    }

    public void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(Player.transform.position, Vector3.down, 4.0f, ~excludeLayer);

        foreach (RaycastHit rayhit in hit)
        {
            Debug.Log(rayhit.transform.gameObject.layer);
            //telling a raycast to look at the ignore raycast feels wrong, but it may work.
            if (rayhit.transform.gameObject.layer == LayerMask.GetMask("Ignore Raycast"))
            {
                currentTerrain = CURRENT_TERRAIN.GROUND;
                break;
            }
            else
            {
                currentTerrain = CURRENT_TERRAIN.WOOD;
                break;
            }
        }
    }

    public void SelectAndPlayFootstep()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.GROUND:
                PlayFootstep(1);
                Debug.Log("MUDDY GROUND");
                break;

            case CURRENT_TERRAIN.WOOD:
                PlayFootstep(0);
                Debug.Log("WOODEN FLOOR");
                break;
        }
    }

}
