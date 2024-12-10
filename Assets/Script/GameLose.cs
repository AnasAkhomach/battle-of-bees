using UnityEngine;
using System.Collections;

public class GameLose : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.touches[i];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        LoadAsync.levelName = SPath.S_GAME;
                        Application.LoadLevel(SPath.S_LOADING);
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Ended:
                        break;
                }
            }
        }
#endif
#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
        if (Input.GetMouseButtonUp(0))
        {
            LoadAsync.levelName = SPath.S_GAME;
            Application.LoadLevel(SPath.S_LOADING);
        }
#endif
    }
}
