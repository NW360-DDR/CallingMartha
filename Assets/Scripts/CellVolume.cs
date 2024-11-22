using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellVolume : MonoBehaviour
{
    public bool gettingCall = false;

    public AudioManager AudioManager;

    [Tooltip("Ranges from zero to three. Three enables saving, the rest are purely for show.")]
    public int cellPower = 3;

    
}
