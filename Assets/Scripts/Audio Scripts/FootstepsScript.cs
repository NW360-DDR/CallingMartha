using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{

    private enum CURRENT_TERRAIN { WOOD, GROUND};

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private FMOD.Studio.EventInstance footsteps;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetermineTerrain();
    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 4.0f)

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
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
