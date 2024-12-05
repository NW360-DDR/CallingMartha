using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{

    private enum CURRENT_TERRAIN { WOOD, GROUND};

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private FMOD.Studio.EventInstance footsteps;

    public CharacterController playerController;

    public float timer = 0.0f;

    [SerializeField]
    float footstepSpeed = 0.3f;


    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponentInParent<CharacterController>();
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
                timer += Time.deltaTime;
            }
        }
    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 4.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
            {
                currentTerrain = CURRENT_TERRAIN.GROUND;
                    break;
            }
            //telling a raycast to look at the ignore raycast feels wrong, but it may work.
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
                break;

            case CURRENT_TERRAIN.WOOD:
                PlayFootstep(0);
                break;
        }
    }

    public void PlayFootstep(int terrain)
    {
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Michael/PlayMichaelFootsteps");
        footsteps.setParameterByName("Terrain", terrain);
        footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        footsteps.start();
        footsteps.release();
    }
}
