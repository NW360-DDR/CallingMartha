using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EclipseTimer : MonoBehaviour
{
    public float timer;
    public float eclipseTimerLength = 30;
    public float redFogTime = 2.5f;
    public bool gameTimerActive = true;
    bool fightBegan = false;
    public bool enteredArena = false;

    private Vector3 targetPos = Vector3.zero;
    public Color RedColor;

    public GameObject lunarMoon;
    public MarthaTestScript Martha;

    public AudioManager AudioManager;

    private void Start()
    {
        StartCoroutine(UpdateMoon());
    }
    void Update()
    {
        //start timer
        timer += gameTimerActive ? Time.deltaTime : 0;

        //check if timer hit eclipse timer length
        // multiply by 60 to make the number into minutes
        if (timer >= (eclipseTimerLength * 60) && !fightBegan)
        {
            gameTimerActive = false;
            fightBegan = !fightBegan;
            Debug.Log("Eclipse happened! Time to die!");
            if (true)
            {
                Martha.KILL();
                AudioManager.EclipseTimerEnd();
            }
        }

        //lunarMoon.transform.localPosition = new Vector3(Mathf.Clamp(timer / eclipseTimerLength, 0f, 1f) , 0, 0);

        //turn fog red over time once it hits fog timer
        if (timer > redFogTime * 60)
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, RedColor, ((Time.deltaTime * 2) / (eclipseTimerLength * 15f)));
    }

    //restarts current loaded scene (will probably change later)
    public IEnumerator Restart()
    {
        Debug.Log("Restarting...");
        yield return new WaitForSeconds(2);
        Debug.Log("Restarted!");
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


