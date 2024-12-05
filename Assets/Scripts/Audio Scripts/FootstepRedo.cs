using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepRedo : MonoBehaviour
{
    private enum CURRENT_TERRAIN { WOOD, GROUND };

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    [SerializeField] FMODUnity.EventReference footstepEvent;
    [SerializeField] FMODUnity.EventReference footstepWoodEvent;

    [SerializeField] float footstepSpeed = 0.3f;

    [SerializeField] GameObject Player;

    [SerializeField] CharacterController playerController;

    float time = 0.0f;

    public void PlayFootstep(int terrain)
    {
       if (terrain == 0)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(footstepWoodEvent, Player);
        }

       if (terrain == 1)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(footstepEvent, Player);
        }
    }


    // Update is called once per frame
    void Update()
    {

        DetermineTerrain();

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

    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            //telling a raycast to look at the ignore raycast feels wrong, but it may work.
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
            {
                currentTerrain = CURRENT_TERRAIN.GROUND;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("WoodFloor"))
            {
                currentTerrain = CURRENT_TERRAIN.WOOD;
                break;
            }
        }
    }
}
