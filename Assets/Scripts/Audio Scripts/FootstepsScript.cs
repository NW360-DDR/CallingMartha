using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{

    private enum CURRENT_TERRAIN { WOOD, GROUND };

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    public FMODUnity.StudioEventEmitter FootstepEmitter;
    public FMODUnity.StudioParameterTrigger footstepParameterTrigger;

    private CharacterController playerController;

    float timer = 0.0f;

    [SerializeField]
    float footstepSpeed = 0.3f;


    // Start is called before the first frame update
    private void Awake()
    {
        playerController = GetComponentInParent<CharacterController>();
        Debug.Log("Found Character Controller");
    }

    // Update is called once per frame
    void Update()
    {
        DetermineTerrain();

        if (playerController.velocity.z < 0 && playerController.velocity.x < 0 && playerController.isGrounded)
        {
            if(timer > footstepSpeed)
            {
                SelectAndPlayFootstep();
                timer = 0.0f;
                Debug.Log("STEP!! STEP!! STEP!!");
            }

            timer += Time.deltaTime;
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

    public void PlayFootstep(int terrain)
    {
        FootstepEmitter.Play();
        //footstepParameterTrigger.TriggerParameter();
        //("Terrain", terrain);

    }
}
