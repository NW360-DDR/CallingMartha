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

    public HealthAndRespawn HealthAndRespawn;

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
        MusicManagement();
    }

    public void MusicManagement()
    {
        if (encounterStarted == true)
        {
            LookForWolves();
        }

        if (HealthAndRespawn.health == 0)
        {
            DeathMusic();
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
        Debug.Log("Wolves are dead, back to spook");
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

        /*if (Enemy.Length > 0)
        {
            Debug.Log("TO BATTLE!!");
            SwitchToEncounter();
        }*/
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

    public void DeathMusic()
    {
        Debug.Log("Player died a tragic death");
        EncounterMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        BossMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ExplorationMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(DeathMusicWait());
    }

    public IEnumerator DeathMusicWait()
    {
        Debug.Log("Start the Countdown");
        yield return new WaitForSeconds(3.0f);
        RestartMusic();
    }

    public void RestartMusic()
    {
        Debug.Log("Any wolves left?");
        if (encounterStarted == true)
        {
            EncounterMusicInstance.start();
        }
        else
        {
            ExplorationMusicInstance.start();
        }
    }
}
