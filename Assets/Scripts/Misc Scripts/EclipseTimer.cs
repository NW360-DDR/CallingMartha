using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EclipseTimer : MonoBehaviour
{
    public float timer;
    public float eclipseTimerLength = 30;
    private bool gameTimerActive = true;
    void Update()
    {
        //start timer
        timer += Time.deltaTime;

        //check if timer hit eclipse timer length
        // multiply by 60 to make the number into minutes
        if (timer > eclipseTimerLength * 60)
        {
            Debug.Log("Eclipse happened! Time to die!");
            gameTimerActive = false;
            StartCoroutine(Restart());
        }
    }
    
    //restarts current loaded scene (will probably change later)
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
