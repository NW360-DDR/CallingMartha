using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep2 : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference footstepEvent;

    [SerializeField] float rate;

    [SerializeField] GameObject Player;

    [SerializeField] CharacterController playerController;

    float time;

    public void PlayFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(footstepEvent, Player);
    }
    

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (playerController.velocity.z < 0 && playerController.velocity.x < 0 && playerController.isGrounded)
        {
            if (time >= rate)
            {
                PlayFootstep();
                time = 0;
            }
        }
    }
}
