using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EclipseTimer : MonoBehaviour
{
    public float timer;
    public float eclipseTimerLength = 30;
    private bool gameTimerActive = true;

    private Vector3 targetPos = Vector3.zero;

    public GameObject lunarMoon;

    private void Start()
    {
        StartCoroutine(UpdateMoon());
    }
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

        //lunarMoon.transform.localPosition = new Vector3(Mathf.Clamp(timer / eclipseTimerLength, 0f, 1f) , 0, 0);
    }
    
    //restarts current loaded scene (will probably change later)
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator UpdateMoon()
    {
        while (lunarMoon.transform.localPosition != targetPos)
        {
            lunarMoon.transform.localPosition = Vector3.MoveTowards(lunarMoon.transform.localPosition, targetPos, (Time.deltaTime / eclipseTimerLength * 1.25f));

            //lunarMoon.transform.localPosition = Vector3.Lerp(lunarMoon.transform.localPosition, targetPos, (Time.deltaTime/eclipseTimerLength)).normalized;

            yield return null;
        }

        yield return new WaitForSeconds(1);
    }
}
