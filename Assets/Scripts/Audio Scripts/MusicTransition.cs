using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{

    public FMOD.Studio.EventInstance ExplorationMusic;
    public FMOD.Studio.EventInstance EncounterMusic;
    public FMOD.Studio.EventInstance BossMusic;

    public bool ExplorationPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        ExplorationMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/PlayExploration");
        EncounterMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/PlayEncounter");
        BossMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/PlayBoss");
    }

    // Update is called once per frame
    void Update()
    {
        if (ExplorationPlaying == false)
        {
            ExplorationMusic.start();
            ExplorationPlaying = true;
        }
    }
}
