using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTrigger : MonoBehaviour
{

    public FootstepTryTheFourth FootstepTryTheFourth;

    void OnCollisionStay(Collision collisionInfo)
    {
        if (gameObject.tag == "Player")
        {
            Debug.Log("Collided with Inside");
        }
    }
}