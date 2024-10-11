using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Campfire : MonoBehaviour
{
    NavMeshObstacle isLit;

    void Light()
    {
        isLit.enabled = true;
    }
}
