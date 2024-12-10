using UnityEngine;
using System.Collections;

public class VideoController : MonoBehaviour
{

    void Awake()
    {
        Screen.SetResolution(800, 480, true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(CoroutinePlayMovie());
        //LoadAsync.levelName = SPath.S_GAMEMENU;
        //Application.LoadLevel(SPath.S_LOADING);

    }

    protected IEnumerator CoroutinePlayMovie()
    {
        Handheld.PlayFullScreenMovie("Final.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
        yield return new WaitForSeconds(0.1f); //Allow time for Unity to pause execution while the movie plays.
        //LoadAsync.levelName = SPath.S_GAMEMENU;
        LoadAsync.levelName = SPath.S_GAMEMENU;
        Application.LoadLevel(SPath.S_LOADING);
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(0))
            {
                LoadAsync.levelName = SPath.S_GAMEMENU;
                Application.LoadLevel(SPath.S_LOADING);
                return;
            }
        }
    }
}
