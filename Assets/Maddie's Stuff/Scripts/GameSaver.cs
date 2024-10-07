using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{

    public SaveData save;
    public GameObject Martha, Player;
    // Start is called before the first frame update
    void Start()
    {
        // Attempt to load the save data. If this fails, fallback onto a brand new save.
        try
        {
            LoadGame();
        }
        catch (Exception)
        {
            save = new SaveData();
        }
    }

    void SaveGame()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/MarthaSave.json", JsonUtility.ToJson(save));
        Debug.Log("Game Saved.");
    }

    void LoadGame()
    {
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(Application.persistentDataPath + "/MarthaSave.json"), save);
        Debug.Log("Game Loaded.");
    }


    void ParseMartha(bool saving)
    {
        var temp = Martha.GetComponent<MarthaTestScript>();

        if (saving) // hey if we're saving the game, maybe apply our saving data.
        {
            save.MarPos = Martha.transform.position;
            save.MarRot = Martha.transform.rotation.eulerAngles;
            save.MarState = temp.brain.GetState();
            save.MarTarget = temp.dest;
        }
        else
        {
            Martha.transform.position = save.MarPos;
            Martha.transform.eulerAngles = save.MarRot;
            switch (save.MarState)
            {
                case "":
                    break;
            }
               
        }
    }
}

[Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;
    public  bool[] Logs = new bool[20];
    public int storyProg;
    public Vector3 MarPos; //
    public Vector3 MarRot; //
    public string MarState; /* */
    public Vector3 MarTarget; //
    public float timeLeft;
    public int FISH;

}
