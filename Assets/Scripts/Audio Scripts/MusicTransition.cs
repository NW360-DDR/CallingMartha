using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{

    public FMODUnity.EventReference ExplorationMusicReference;
    public FMOD.Studio.EventInstance ExplorationMusicInstance;
    //public string ExplorationMusicName;

    public FMODUnity.EventReference EncounterMusicReference;
    public FMOD.Studio.EventInstance EncounterMusicInstance;


    public FMODUnity.EventReference BossMusicReference;
    public FMOD.Studio.EventInstance BossMusicInstance;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        ExplorationMusicInstance = FMODUnity.RuntimeManager.CreateInstance(ExplorationMusicReference);
        EncounterMusicInstance = FMODUnity.RuntimeManager.CreateInstance(EncounterMusicReference);
        BossMusicInstance = FMODUnity.RuntimeManager.CreateInstance(BossMusicReference);

        ExplorationMusicInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SwitchToEncounter()
    {
        Debug.Log("Wolves tryin ta kill ya oooh spooky");
        ExplorationMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        EncounterMusicInstance.start();
    }
}
