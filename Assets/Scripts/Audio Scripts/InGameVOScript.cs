using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame3Start : MonoBehaviour
{
    public AudioSource InGameClip;
    public float ClipLength;
    public Collider BoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        

    }


    // Update is called once per frame
    void Update()
    {

        
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InGameClip.Play();
            StartCoroutine(WaittoEnd());
            BoxCollider.enabled = false;
            
        }
    }

    public IEnumerator WaittoEnd()
    {
        yield return new WaitForSeconds(ClipLength);
        Destroy(this.gameObject);
    }
}
