using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuScreen : MonoBehaviour
{
    public enum MState
    {
        MAIN_MENU, OPTION, INFOR, EXIT, HELPER
    }

    public static MState State { get; set; }
    public Transform ongmenu1, ongmenu2;
    float stateTime = 7;
    float stateTime2 = 5;
    //dfPanel mainPanelOption;
    AudioController audioController;



    int count = 0;
    int rad;
    void Awake()
    {
        initPref();
    }

    void initPref()
    {
        MyPref.Reset_Component();
        MyPref.load();
    }

    void Start()
    {
        //Google Admob
        
        rad = Random.Range(0, 10);

        Time.timeScale = 1;
        //Login Google Service
        // Google Service

        Social.localUser.Authenticate((bool success) =>
        {
        });

        //mainPanelOption = GameObject.FindGameObjectWithTag(SPath.TAG_MAIN_OPTION).GetComponent<dfPanel>();
        audioController = GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>();
        audioController.playMusic(0);

        //mainPanelOption.Width = 0;
        //mainPanelOption.Height = 0;
        State = MState.MAIN_MENU;
    }

    // Update is called once per frame
    void Update()
    {

        //if (rad >= 7)
        //{
        //    if (GoolgeAdmob.interstitial.IsLoaded() && count == 0)
        //    {
        //        GoolgeAdmob.interstitial.Show();
        //        count = 1;
        //    }
        //}
        switch (State)
        {
            case MState.MAIN_MENU:
                stateTime += Time.deltaTime;
                stateTime2 += Time.deltaTime;
                if (stateTime >= 8)
                {
                    Instantiate(ongmenu1, new Vector3(-5, -3, 3), Quaternion.identity);
                    stateTime = 0;
                }
                if (stateTime2 >= 16)
                {
                    Instantiate(ongmenu2, new Vector3(-5, -3, 3), Quaternion.identity);
                    stateTime2 = 0;
                }
                if (Input.GetKey(KeyCode.Escape))
                {
                    if (!GameObject.Find("PanelExitGame").GetComponent<dfTweenGroup>().Tweens[0].IsPlaying)
                    {
                        GameObject.Find("PanelExitGame").GetComponent<dfTweenGroup>().Tweens[0].Play();
                        State = MState.EXIT;
                    }
                }
                break;
            case MState.OPTION:
                break;
            case MState.INFOR:
                break;
            case MState.EXIT:
                break;
            case MState.HELPER:
                break;
        }



    }

    public void OnResumeGame()
    {
        if (State == MState.EXIT)
        {
            if (!GameObject.Find("PanelExitGame").GetComponent<dfTweenGroup>().Tweens[1].IsPlaying)
            {
                GameObject.Find("PanelExitGame").GetComponent<dfTweenGroup>().Tweens[1].Play();
                State = MState.MAIN_MENU;
            }
        }
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    void OnDestroy()
    {

    }
}
