using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{

    public FMODUnity.EventReference ExplorationMusicReference;
    public FMOD.Studio.EventInstance ExplorationMusicInstance;

    public FMODUnity.EventReference EncounterMusicReference;
    public FMOD.Studio.EventInstance EncounterMusicInstance;

    public FMODUnity.EventReference BossMusicReference;
    public FMOD.Studio.EventInstance BossMusicInstance;

    public GameObject Player;

    public GameObject[] Enemy;

    public bool encounterStarted = false;

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
        if (encounterStarted == true)
        {
            LookForWolves();
        } 

    }

    public void SwitchToEncounter()
    {
        Debug.Log("Wolves tryin ta kill ya oooh spooky");
        ExplorationMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        EncounterMusicInstance.start();
        encounterStarted = true;
    }

    public void SwitchOutOfEncounter()
    {
        EncounterMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ExplorationMusicInstance.start();
    }

    public void LookForWolves()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Wolf");

        if (Enemy.Length == 0)
        {
            SwitchOutOfEncounter();
            encounterStarted = false;
            Debug.Log("Oh wow how did we get this far");
        }
    }

    public void BeginMarthaMusic()
    {
        ExplorationMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        EncounterMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        BossMusicInstance.start();
    }

    public void EndMarthaMusic()
    {
        /*if needed, called when player dies to reset to Exploration Music*/
    }
}
