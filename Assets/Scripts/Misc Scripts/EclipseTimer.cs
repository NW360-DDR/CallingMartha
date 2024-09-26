using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EclipseTimer : MonoBehaviour
{
    public float timer;
    public float eclipseTimerLength = 30;
    private bool gameTimerActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > eclipseTimerLength * 60)
        {
            Debug.Log("Eclipse happened! Time to die!");
            gameTimerActive = false;
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
