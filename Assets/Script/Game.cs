using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

    void Awake()
    {
        Screen.SetResolution(800, 480, true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    // Use this for initialization
    void Start()
    {
       // LoadAsync.levelName = SPath.S_VIDEOLOADING;
        Application.LoadLevel(SPath.S_VIDEOLOADING);
    }
}
