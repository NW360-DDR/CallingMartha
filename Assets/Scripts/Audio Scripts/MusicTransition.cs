using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{

    public FMODUnity.EventReference ExplorationMusic;
    public FMODUnity.EventReference EncounterMusic;
    public FMODUnity.EventReference BossMusic;

    public bool ExplorationPlaying = false;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (ExplorationPlaying == false)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(ExplorationMusic, Player);

            ExplorationPlaying = true;
        }
    }
}
