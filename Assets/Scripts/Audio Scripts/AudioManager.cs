using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public string HurtEvent = "";

    public string DeathEvent = "";

    // Start is called before the first frame update


    

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayerHurt()
    {
        FMODUnity.RuntimeManager.PlayOneShot(HurtEvent, transform.position);
    }

    public void PlayerDead()
    {
        FMODUnity.RuntimeManager.PlayOneShot(DeathEvent, transform.position);
    }

   
}
