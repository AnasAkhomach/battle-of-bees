using UnityEngine;
using System.Collections;

public class WinnerSreen : MonoBehaviour
{

    int newLevel = 0;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey(SPath.LEVEL))
            newLevel = PlayerPrefs.GetInt(SPath.LEVEL) + 1;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
        TouchPhaseClick();
#endif
#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
        TouchMouseClick();
#endif
    }

    void TouchMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (newLevel != 0 && PlayerPrefs.HasKey(SPath.LEVEL))
            {
                PlayerPrefs.SetInt(SPath.LEVEL, newLevel);
                newLevel = 0;
            }
            LoadAsync.levelName = SPath.S_GAMESCREEN;
            Application.LoadLevel(SPath.S_LOADING);
        }
    }

    void TouchPhaseClick()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.touches[i];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (newLevel != 0 && PlayerPrefs.HasKey(SPath.LEVEL))
                        {
                            PlayerPrefs.SetInt(SPath.LEVEL, newLevel);
                            newLevel = 0;
                        }
                        LoadAsync.levelName = SPath.S_GAMESCREEN;
                        Application.LoadLevel(SPath.S_LOADING);
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Ended:
                        break;
                }
            }
        }
    }
}
