using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameScreen : MonoBehaviour
{

    public enum GState
    {
        PAUSE, RUNNING, GAME_WIN, GAME_LOSE, SHOP, WARNING
    }
    public GState State { get; set; }

    Vector3 posTouched = Vector3.zero;
    public Transform bulletTranform;
    BulletController bulletController;
    GameObject playerObject;
    SwitchGun switchGun;
    PlayerController playerController;
    Vector3 posCurrent;

    public List<GameObject> listOngMat;
    public List<GameObject> listOngBom;

    BossController bossScript;
    public Transform boss;
    //public Transform ongBom;
    public Transform ongChich;
    // public Transform ongmat;
    public Transform iProtect;
    public Transform iBullet;
    public Transform iDef;
    public Transform iHp;
    public Transform danroi;
    public Transform bossDemo;
    float stateTime, stateTime2, stateTime3, time, timeItemIprotect, timeItemBullet, timeItemDef, timeItemHp;
    float z;
    int levelCurrent;
    int totalTime;
    bool createBoss;
    float score;

    TimeText timeText;

    GameObject objectAudio;
    AudioController audioController;

    float timeWarning;
    int countWarning;

    public Transform labelWarning;
    public Transform labelWarningHalfTime;
    public Transform labelWarningBoss;
    //public GoogleAnalyticsV3 ga;
    void Awake()
    {


        SPath.Score_current = 0;
        MyPref.SaveGame(false);

#if UNITY_EDITOR || UNITY_WEBPLAYER
        rect = new Rect(0, 0, 0, 0);
        //if (GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().enable)
        //  GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().enable = false;
#else
       rect = new Rect(-4, -2.4f, 2, 2);
       // if (!GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().enable)
         //   GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().enable = true;
#endif
        timeText = gameObject.GetComponent<TimeText>();
        bulletController = bulletTranform.GetComponent<BulletController>();
        playerObject = GameObject.FindGameObjectWithTag(SPath.TAG_PLAYER);
        playerController = playerObject.GetComponent<PlayerController>();
        switchGun = playerObject.GetComponent<SwitchGun>();
        bossScript = boss.GetComponent<BossController>();
        if (PlayerPrefs.HasKey(SPath.LEVEL))
            levelCurrent = PlayerPrefs.GetInt(SPath.LEVEL);
        else
        {
            PlayerPrefs.SetInt(SPath.LEVEL, 1);
            levelCurrent = PlayerPrefs.GetInt(SPath.LEVEL);
        }
        totalTime = 45 + Mathf.Clamp(5 * (levelCurrent - 1), 0, 35);
        createBoss = false;
        SetGun1();
        SPath.CHECK_BOSS = 0;
        InitGameContains();
    }

    void InitGameContains()
    {
        GameContains.Score = MyPref.Get_Score_Current();

        //Ong Bom
        GameContains.Count_OngBom = MyPref.Get_Count_OngBom();
        GameContains.Count_Bonus_Bom = 0;

        //OngChich
        GameContains.Count_OngChich = MyPref.Get_Count_OngChich();
        GameContains.Count_Bonus_OngChich = 0;
    }

    public void HideShop()
    {
        State = GState.RUNNING;
    }

    int indexMusic;
    // Use this for initialization
    void Start()
    {
        objectAudio = GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO);
        audioController = objectAudio.GetComponent<AudioController>();
        indexMusic = 1;
        // audioController.playMusic(4);
        bullet_gun = new int[4];
        if (levelCurrent < 3)
            GameObject.FindGameObjectWithTag("btgun4").GetComponent<dfButton>().IsEnabled = false;
        else
            GameObject.FindGameObjectWithTag("btgun4").GetComponent<dfButton>().IsEnabled = true;

        //z = ongBom.transform.position.z;
        InitEnemy();
        InitItem();

        if (levelCurrent == 1)
        {
            State = GState.RUNNING;
            audioController.playMusic(indexMusic);
            getDataFormPref();
            GameObject.FindGameObjectWithTag("shop").GetComponent<dfPanel>().IsVisible = false;
            if (!labelWarning.GetComponent<dfLabel>().IsVisible)
            {
                labelWarning.GetComponent<dfLabel>().IsVisible = true;
                labelWarning.GetComponent<TextEffectWarning>().ShowText();
            }
        }
        else
        {
            State = GState.SHOP;
            GameObject.FindGameObjectWithTag("shop").GetComponent<dfPanel>().IsVisible = true;
        }
    }

    public void PlayAudioRandom()
    {
        audioController.StopMusic(4);
        audioController.playMusic(indexMusic);
    }

    void InitItem()
    {
        if (levelCurrent == 1)
            InvokeRepeating("CreateItemBullet", 20f, 20f);

        if (levelCurrent == 2)
        {
            InvokeRepeating("CreateItemBullet", 8f, 15f);
            InvokeRepeating("CreateItemHp", 11f, 20f);
        }
        if (levelCurrent == 3)
        {
            InvokeRepeating("CreateItemBullet", 8f, 15f);
            InvokeRepeating("CreateItemHp", 11f, 20f);
            InvokeRepeating("CreateItemDef", 15f, 20f);
        }
        if (levelCurrent >= 4)
        {
            InvokeRepeating("CreateItemProtect", 10, totalTime / 2f);
            InvokeRepeating("CreateItemBullet", 1f, totalTime / 5f);
            InvokeRepeating("CreateItemDef", 15f, totalTime / 4f);
            InvokeRepeating("CreateItemHp", 8f, totalTime / 3f);
        }
    }

    void InitEnemy()
    {
        InvokeRepeating("CreateOngBom", 1f, 3f);
        InvokeRepeating("CreateOngMat", 1f, 7f);
        if (levelCurrent > 2)
            InvokeRepeating("CreateOngChich", 1f, 4f);
        if (levelCurrent == 3)
            Invoke("CreateBossDemo", 20);
    }

    void CreateBossDemo()
    {
        Instantiate(bossDemo);
    }

    void Update()
    {
        //Android
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Menu))
        {
            if (State == GState.RUNNING)
            {
                GameObject.FindGameObjectWithTag("buttonpause").GetComponent<ButtonEvents>().PauseGame();
                State = GState.PAUSE;
                return;
            }
        }

        if (GameObject.Find("btGun1").GetComponent<dfButton>().NormalBackgroundColor.g == 0)
        {
            GameObject.Find("btGun2").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
            GameObject.Find("btGun3").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
            GameObject.Find("btGun4").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        }
        if (GameObject.Find("btGun2").GetComponent<dfButton>().NormalBackgroundColor.g == 0)
        {
            GameObject.Find("btGun1").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
            GameObject.Find("btGun3").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
            GameObject.Find("btGun4").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        }
        if (GameObject.Find("btGun3").GetComponent<dfButton>().NormalBackgroundColor.g == 0)
        {
            GameObject.Find("btGun2").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
            GameObject.Find("btGun1").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
            GameObject.Find("btGun4").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        }
        if (GameObject.Find("btGun4").GetComponent<dfButton>().NormalBackgroundColor.g == 0)
        {
            GameObject.Find("btGun2").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
            GameObject.Find("btGun3").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
            GameObject.Find("btGun1").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        }
        switch (State)
        {
            case GState.SHOP:
                if (GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible)
                    GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible = false;
                Time.timeScale = 0;
                break;
            case GState.PAUSE:
                if (GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible)
                    GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible = false;
                Time.timeScale = 0;
                // Debug.Log("hehehe");
                break;
            case GState.RUNNING:
#if UNITY_EDITOR || UNITY_WEBPLAYER
                if (GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible)
                    GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible = false;
#else
                 if (!GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible)
                    GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible = true;
#endif

                if (totalTimeCurrent <= totalTime / 2f && countWarning == 0 && levelCurrent > 1)
                {
                    countWarning = 1;
                    CallGameWarning1();
                }
                if (totalTimeCurrent <= 5 && countWarning == 1 && levelCurrent >= 5)
                {
                    countWarning = 2;
                    CallGameWarning2();
                }

                if (isTouchScreen)
                {

                    /*
                     *-----------------Web vs Editor 
                     */
                    if (gun == 2)
                    {
                        if (bullet_gun[gun - 1] > 0)
                        {
                            timeCreateBullet += Time.deltaTime;
                            if (timeCreateBullet >= 0.2f && (!rect.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition))))
                            {
                                if (switchGun.type != SwitchGun.Type.FIRE)
                                {
                                    createBullet(2, false);
                                    switchGun.type = SwitchGun.Type.FIRE;
                                }
                            }
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                        }
                    }

                    /*
                    *-------------Mobile Device
                    */
                    //if (!rect.Contains(Camera.main.ScreenToWorldPoint(touchdrag.position)))
                    //{
                    //    if (gun == 2)
                    //    {
                    //        if (bullet_gun[gun - 1] > 0)
                    //        {
                    //            timeCreateBullet += Time.deltaTime;
                    //            if (timeCreateBullet >= 0.2f)
                    //            {
                    //                if (switchGun.type != SwitchGun.Type.FIRE)
                    //                {
                    //                    createBullet(touchdrag, 2, false);
                    //                    switchGun.type = SwitchGun.Type.FIRE;
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                    //        }
                    //    }
                    //}
                }
                Time.timeScale = 1;
                break;
            case GState.GAME_WIN:
                if (GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible)
                    GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible = false;
                Time.timeScale = 0;
                break;
            case GState.GAME_LOSE:
                if (GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible)
                    GameObject.FindGameObjectWithTag("joystick").GetComponent<EasyJoystick>().visible = false;
                Time.timeScale = 0;
                break;
            case GState.WARNING:
                timeWarning += Time.deltaTime;
                if (timeWarning >= 5f)
                {
                    State = GState.RUNNING;
                    timeWarning = 0;
                }

                break;
        }

        timeText.time = totalTimeCurrent;
        timeText.level = levelCurrent;
#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_BLACKBERRY
        touchPhaseScreen();
#endif
#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
        touchMouseButton();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetGun1();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetGun2();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetGun3();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetGun4();
#endif
        createEnemy();
        createItem();
        EventAddItemBullet();

    }


    void CallGameWarning1()
    {
        if (State == GState.WARNING)
            return;
        State = GState.WARNING;
        timeWarning = 0;
        Invoke("ShowWarning1", 2f);
    }
    void CallGameWarning2()
    {
        if (State == GState.WARNING)
            return;
        State = GState.WARNING;
        timeWarning = 0;
        Invoke("ShowWarning2", 2f);
    }

    void ShowWarning2()
    {
        if (!labelWarningBoss.GetComponent<dfLabel>().IsVisible)
        {
            labelWarningBoss.GetComponent<dfLabel>().IsVisible = true;
            labelWarningBoss.GetComponent<TextEffectWarningBoss>().ShowText();
        }
    }

    void ShowWarning1()
    {
        if (!labelWarningHalfTime.GetComponent<dfLabel>().IsVisible)
        {
            labelWarningHalfTime.GetComponent<dfLabel>().IsVisible = true;
            labelWarningHalfTime.GetComponent<TextEffectWarningHalfTime>().ShowText();
        }
    }

    void EventAddItemBullet()
    {
        if (playerController.AddBullet)
        {
            int rand = Random.Range(1, 4);
            MyPref.setGun(rand);
            switch (rand)
            {
                case 1:
                    SetGun1();
                    bullet_gun[gun - 1] += SPath.NUMBER_BULLET_MAX_2;
                    break;
                case 2:
                    SetGun2();
                    bullet_gun[gun - 1] += SPath.NUMBER_BULLET_MAX;
                    break;
                case 3:
                    SetGun3();
                    bullet_gun[gun - 1] += SPath.NUMBER_BULLET_MIN;
                    break;
            }
            playerController.AddBullet = false;
        }
    }

    void CreateItemProtect()
    {
        Instantiate(iProtect, new Vector3(randomBool() ? Camera.main.transform.position.x - 5 : Camera.main.transform.position.x + 5, Random.Range(-2f, 2.5f), 0), Quaternion.identity);
    }
    void CreateItemBullet()
    {
        Instantiate(iBullet, new Vector3(randomBool() ? Camera.main.transform.position.x - 5 : Camera.main.transform.position.x + 5, Random.Range(-2f, 2.5f), 0), Quaternion.identity);
    }
    void CreateItemDef()
    {
        Instantiate(iDef, new Vector3(randomBool() ? Camera.main.transform.position.x - 5 : Camera.main.transform.position.x + 5, Random.Range(-2f, 2.5f), 0), Quaternion.identity);
    }
    void CreateItemHp()
    {
        Instantiate(iHp, new Vector3(randomBool() ? Camera.main.transform.position.x - 5 : Camera.main.transform.position.x + 5, Random.Range(-2f, 2.5f), 0), Quaternion.identity);
    }

    private void createItem()
    {
        timeItemIprotect += Time.deltaTime;
        timeItemBullet += Time.deltaTime;
        timeItemDef += Time.deltaTime;
        timeItemHp += Time.deltaTime;
    }

    int totalTimeCurrent = 3;
    void OnGUI()
    {
        totalTimeCurrent = (totalTime - (int)time);
        if (totalTimeCurrent <= 0) totalTimeCurrent = 0;
        if (((levelCurrent < 5 && totalTimeCurrent <= 0) || (levelCurrent >= 5 && totalTimeCurrent <= 0 && SPath.CHECK_BOSS == 1)) && State != GState.GAME_WIN)
            callGameWin();

        for (int id = 0; id < 4; id++)
        {
            if (bullet_gun[id] <= 0)
                bullet_gun[id] = 0;
        }
        timeText.totalBullet = bullet_gun[gun - 1];
    }

    public void SetGun1()
    {

        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
        GameObject.Find("btGun1").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 0, 0, 1);
        GameObject.Find("btGun2").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.Find("btGun3").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.Find("btGun4").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        gun = 1;
        MyPref.setGun(1);
    }

    public void SetGun2()
    {
        GameObject.Find("btGun2").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 0, 0, 1);
        GameObject.Find("btGun1").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.Find("btGun3").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.Find("btGun4").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
        gun = 2;
        MyPref.setGun(2);
    }

    public void SetGun3()
    {
        GameObject.Find("btGun3").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 0, 0, 1);
        GameObject.Find("btGun2").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.Find("btGun1").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.Find("btGun4").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
        gun = 3;
        MyPref.setGun(3);
    }

    public void SetGun4()
    {
        GameObject.Find("btGun4").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 0, 0, 1);
        GameObject.Find("btGun2").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.Find("btGun1").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.Find("btGun3").GetComponent<dfButton>().NormalBackgroundColor = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
        gun = 4;
        MyPref.setGun(4);
    }

    void callGameWin()
    {
        //GoolgeAdmob.bannerView.Show();
        //gameObject.GetComponent<AdGoogle>().bannerView.Show();
        // MyPref.Save_Bonus_OngBom(GameContains.Count_Bonus_Bom);
        //MyPref.Save_Bonus_OngChich(GameContains.Count_Bonus_OngChich);
        MyPref.Save_Count_OngBom(GameContains.Count_OngBom);
        MyPref.Save_Count_OngChich(GameContains.Count_OngChich);
        MyPref.Save_Score_Current(GameContains.Score);
        MyPref.SaveCoin(SPath.Score_current / 10);
        MyPref.SaveGame(true);
        audioController.StopMusic(indexMusic);
        audioController.playMusic(2);
        GameObject.FindGameObjectWithTag(SPath.TAG_PANEL_GAMEOVER).GetComponent<dfPanel>().IsVisible = true;
        GameObject.FindGameObjectWithTag(SPath.TAG_PANEL_GAMEOVER).GetComponent<ShowGameOver>().show();
        saveDataForWin();
        pushDataToPref();
        State = GState.GAME_WIN;
        totalTimeCurrent = 0;
        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        Social.ReportScore((int)MyPref.getScore(), "CgkIjcL-1YIFEAIQBg", (bool success) =>
        {
        });
    }

    public void callGameLose()
    {
        //GoolgeAdmob.bannerView.Show();
        //gameObject.GetComponent<AdGoogle>().bannerView.Show();
        // MyPref.Save_Bonus_OngBom(GameContains.Count_Bonus_Bom);
        // MyPref.Save_Bonus_OngChich(GameContains.Count_Bonus_OngChich);
        MyPref.Save_Count_OngBom(GameContains.Count_OngBom);
        MyPref.Save_Count_OngChich(GameContains.Count_OngChich);
        MyPref.Save_Score_Current(GameContains.Score);
        MyPref.SaveCoin(SPath.Score_current / 10);
        State = GState.GAME_LOSE;
        GameObject.FindGameObjectWithTag(SPath.TAG_PANEL_GAME_LOSE).GetComponent<dfPanel>().IsVisible = true;
        GameObject.FindGameObjectWithTag(SPath.TAG_PANEL_GAME_LOSE).GetComponent<ShowGameLose>().showLose();
        audioController.StopMusic(indexMusic);
        audioController.playMusic(3);
        saveDataForLose();
        pushDataToPref();
        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        Social.ReportScore((int)MyPref.getScore(), "CgkIjcL-1YIFEAIQBg", (bool success) =>
        {
        });
    }

    void saveDataForLose()
    {
        //remove data without score
        // DeleteKey_Pref();
        MyPref.setScore(MyPref.Get_Score_Current());
    }

    public void DeleteKey_Pref()
    {
        MyPref.Reset_Component();
    }

    void saveDataForWin()
    {
        GameObject.FindGameObjectWithTag(SPath.TAG_PLAYER).GetComponent<PlayerController>().saveHP();
        MyPref.setScore(MyPref.Get_Score_Current());
        PlayerPrefs.SetInt(SPath.LEVEL, levelCurrent + 1);
    }

    Rect rect;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(rect.x, rect.y), new Vector3(rect.width, rect.height));
    }

    bool isTouchScreen;
    Vector3 posDrag;
    Touch touchdrag;
    private void touchPhaseScreen()
    {
        if (Time.timeScale == 0)
            return;

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.touches[i];

                Debug.Log(posDrag);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchdrag = touch;
                        isTouchScreen = true;
                        if (!(rect.Contains(Camera.main.ScreenToWorldPoint(touch.position))))
                        {
                            if (gun == 4)
                            {
                                //if (bullet_gun[gun - 1] > 0)
                                //{
                                //    if (switchGun.type != SwitchGun.Type.FIRE)
                                //    {
                                //        createBullet(touch, 1, true);
                                //        createBullet(touch, 2, true);
                                //        createBullet(touch, 3, true);
                                //        switchGun.type = SwitchGun.Type.FIRE;
                                //    }
                                //}
                                //else
                                //{
                                //    GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                                //}
                                if (bullet_gun[gun - 1] > 0)
                                {
                                    if (switchGun.type != SwitchGun.Type.FIRE)
                                    {
                                        createBullet(touch, 1, true);
                                        createBullet(touch, 2, true);
                                        createBullet(touch, 3, true);
                                        switchGun.type = SwitchGun.Type.FIRE;
                                    }
                                }
                                else
                                {
                                    GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                                }
                            }
                            else if (gun == 2)
                            {
                                if (bullet_gun[gun - 1] > 0)
                                {
                                    if (switchGun.type != SwitchGun.Type.FIRE)
                                    {
                                        createBullet(touch, 2, false);
                                        switchGun.type = SwitchGun.Type.FIRE;
                                    }
                                }
                                else
                                {
                                    GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                                }
                            }
                            else
                            {
                                if (bullet_gun[gun - 1] > 0)
                                {
                                    if (switchGun.type != SwitchGun.Type.FIRE)
                                    {
                                        createBullet(touch, 2, true);
                                        switchGun.type = SwitchGun.Type.FIRE;
                                    }
                                }
                                else
                                {
                                    GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                                }
                            }


                            playerRotation(touch);
                        }
                        break;
                    case TouchPhase.Moved:
                        touchdrag = touch;
                        isTouchScreen = true;
                        if (!rect.Contains(Camera.main.ScreenToWorldPoint(touch.position)))
                        {
                            //if (gun == 2)
                            //{
                            //    if (bullet_gun[gun - 1] > 0)
                            //    {
                            //        timeCreateBullet += Time.deltaTime;
                            //        if (timeCreateBullet >= 0.2f)
                            //        {
                            //            if (switchGun.type != SwitchGun.Type.FIRE)
                            //            {
                            //                createBullet(touch, 2,false);
                            //                switchGun.type = SwitchGun.Type.FIRE;
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {
                            //        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                            //    }
                            //}
                            playerRotation(touch);
                        }
                        break;
                    case TouchPhase.Ended:
                        touchdrag = touch;
                        isTouchScreen = false;
                        break;
                }
            }
        }
    }


    float timeCreateBullet;
    private void createBullet(int id, bool b)
    {
        if (rect.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            return;
        if (playerController.transform.localScale.y > 0)
            Instantiate(danroi, new Vector3(playerController.transform.position.x + 0.6f, playerController.transform.position.y - 0.2f, 0), Quaternion.identity);
        else
            Instantiate(danroi, new Vector3(playerController.transform.position.x, playerController.transform.position.y - 0.2f, 0), Quaternion.identity);
        timeCreateBullet = 0;
        bullet_gun[gun - 1] -= 1;
        posTouched = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        BulletController blControl;
        if (b)
        {
            var bullet = Instantiate(bulletTranform, new Vector3(playerController.transform.position.x, playerController.transform.position.y + 0.1f), Quaternion.identity) as Transform;
            blControl = bullet.GetComponent<BulletController>();
        }
        else
        {
            var bullet = Instantiate(bulletTranform, new Vector3(playerController.transform.position.x, playerController.transform.position.y), Quaternion.identity) as Transform;
            blControl = bullet.GetComponent<BulletController>();
        }


        blControl.Target = posTouched;


        posCurrent = playerController.transform.position;
        Vector2 pos = posCurrent - posTouched;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        switch (id)
        {
            case 1:
                blControl.alpha = angle + 180 + 7;
                break;
            case 2:
                blControl.alpha = angle + 180;
                break;
            case 3:
                blControl.alpha = angle + 180 - 7;
                break;
        }

        blControl.angle = angle + 180;
        playerController.setFire();
        audioController.playSound(2);
    }

    Vector2 vt2, vt;
    private void createBullet(Touch touch, int id, bool b)
    {
        if (rect.Contains(Camera.main.ScreenToWorldPoint(touch.position)))
            return;
        if (playerController.transform.localScale.y > 0)
            Instantiate(danroi, new Vector3(playerController.transform.position.x + 0.6f, playerController.transform.position.y - 0.2f, 0), Quaternion.identity);
        else
            Instantiate(danroi, new Vector3(playerController.transform.position.x, playerController.transform.position.y - 0.2f, 0), Quaternion.identity);
        timeCreateBullet = 0;
        bullet_gun[gun - 1] -= 1;
        posTouched = Camera.main.ScreenToWorldPoint(touch.position);

        BulletController blControl;
        if (b)
        {
            var bullet = Instantiate(bulletTranform, new Vector3(playerController.transform.position.x, playerController.transform.position.y + 0.1f), Quaternion.identity) as Transform;
            blControl = bullet.GetComponent<BulletController>();
        }
        else
        {
            var bullet = Instantiate(bulletTranform, new Vector3(playerController.transform.position.x, playerController.transform.position.y), Quaternion.identity) as Transform;
            blControl = bullet.GetComponent<BulletController>();
        }
        blControl.Target = posTouched;
        posCurrent = playerController.transform.position;
        vt2 = posCurrent - posTouched;
        float angle = Mathf.Atan2(vt2.y, vt2.x) * Mathf.Rad2Deg;
        switch (id)
        {
            case 1:
                blControl.alpha = angle + 180 + 7;
                break;
            case 2:
                blControl.alpha = angle + 180;
                break;
            case 3:
                blControl.alpha = angle + 180 - 7;
                break;
        }
        blControl.angle = angle + 180;
        playerController.setFire();
        audioController.playSound(2);
    }

    //thiet lap 1 bien luu tam thoi tu file
    int gun;
    int[] bullet_gun;

    public void getDataFormPref()
    {
        gun = MyPref.getGun();
        for (int id = 0; id < 4; id++)
        {
            bullet_gun[id] = MyPref.getDan(id + 1);
        }
    }

    public void pushDataToPref()
    {
        MyPref.setGun(gun);
        for (int id = 0; id < 4; id++)
        {
            MyPref.setDan(id + 1, bullet_gun[id]);
        }
    }

    void touchMouseButton()
    {
        posDrag = Input.mousePosition;
        if (Time.timeScale == 0)
            return;
        if (Input.GetMouseButton(0))
        {
            isTouchScreen = true;
            //if (gun == 2)
            //{
            //    if (bullet_gun[gun - 1] > 0)
            //    {
            //        timeCreateBullet += Time.deltaTime;
            //        if (timeCreateBullet >= 0.2f && (!rect.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition))))
            //        {
            //            if (switchGun.type != SwitchGun.Type.FIRE)
            //            {
            //                createBullet(2, false);
            //                switchGun.type = SwitchGun.Type.FIRE;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
            //    }
            //}
            playerRotation();
        }

        if (Input.GetMouseButtonDown(0))
        {
            isTouchScreen = true;
            if (!(rect.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition))))
            {
                if (gun == 4)
                {
                    if (bullet_gun[gun - 1] > 0)
                    {
                        if (switchGun.type != SwitchGun.Type.FIRE)
                        {
                            createBullet(1, true);
                            createBullet(2, true);
                            createBullet(3, true);
                            switchGun.type = SwitchGun.Type.FIRE;
                        }
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                    }
                }
                else if (gun == 2)
                {
                    if (bullet_gun[gun - 1] > 0)
                    {
                        if (switchGun.type != SwitchGun.Type.FIRE)
                        {
                            createBullet(2, false);
                            switchGun.type = SwitchGun.Type.FIRE;
                        }
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                    }
                }
                else
                {
                    if (bullet_gun[gun - 1] > 0)
                    {
                        if (switchGun.type != SwitchGun.Type.FIRE)
                        {
                            createBullet(2, true);
                            switchGun.type = SwitchGun.Type.FIRE;
                        }
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                    }
                }


                playerRotation();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouchScreen = false;
        }
    }

    private void playerRotation()
    {
        Vector2 p1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 p2 = playerController.transform.position;
        Vector2 v2 = p2 - p1;
        float alpha = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        //xoay nhan vat
        if (alpha >= -90 && alpha <= 90)
            playerController.transform.localScale = new Vector3(0.5f, -0.5f, 1);
        else
            playerController.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        playerController.transform.rotation = Quaternion.Euler(new Vector3(0, 0, alpha + 180));
    }
    private void playerRotation(Touch touch)
    {
        Vector2 p1 = Camera.main.ScreenToWorldPoint(touch.position);
        Vector2 p2 = playerController.transform.position;
        Vector2 v2 = p2 - p1;
        float alpha = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        //xoay nhan vat
        if (alpha >= -90 && alpha <= 90)
            playerController.transform.localScale = new Vector3(0.5f, -0.5f, 1);
        else
            playerController.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        playerController.transform.rotation = Quaternion.Euler(new Vector3(0, 0, alpha + 180));
    }

    List<int> idOngBom;
    void CreateOngBom()
    {
        if (State == GState.WARNING)
            return;
        idOngBom = new List<int>();
        for (int i = 0; i < listOngBom.Count; i++)
        {
            if (!listOngBom[i].active)
                idOngBom.Add(i);
        }
        if (idOngBom.Count >= 1)
            listOngBom[idOngBom[Random.Range(0, idOngBom.Count)]].SetActive(true);
        idOngBom.Clear();
        //Instantiate(ongBom, new Vector3(randomBool() ? Camera.main.transform.position.x - 5 : Camera.main.transform.position.x + 5, Random.Range(-2, 2), z), Quaternion.identity);
    }
    List<int> idOngMat;
    void CreateOngMat()
    {
        idOngMat = new List<int>();
        for (int i = 0; i < listOngMat.Count; i++)
        {
            if (!listOngMat[i].active)
                idOngMat.Add(i);
        }
        if (idOngMat.Count >= 1)
            listOngMat[idOngMat[Random.Range(0, idOngMat.Count)]].SetActive(true);
        idOngMat.Clear();
        //Instantiate(ongmat, new Vector3(randomBool() ? Camera.main.transform.position.x - 5 : Camera.main.transform.position.x + 5, Random.Range(-2, 3), z), Quaternion.identity);
    }
    void CreateOngChich()
    {
        if (State == GState.WARNING)
            return;
        Instantiate(ongChich, new Vector3(randomBool() ? Camera.main.transform.position.x - 5 : Camera.main.transform.position.x + 5, 0, z), Quaternion.identity);
    }

    private void createEnemy()
    {
        stateTime += Time.deltaTime;
        stateTime2 += Time.deltaTime;
        stateTime3 += Time.deltaTime;
        time += Time.deltaTime;
        if (levelCurrent >= 5)
        {
            if (totalTimeCurrent <= 2 && !createBoss)
            {
                Instantiate(boss, new Vector3(4, 4, z), Quaternion.identity);
                createBoss = true;
            }
        }
    }

    bool randomBool()
    {
        int rand = Random.Range(0, 2);
        return rand == 1 ? true : false;
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (State == GState.SHOP)
            return;
        //  pushDataToPref();
        GameObject.FindGameObjectWithTag("buttonpause").GetComponent<ButtonEvents>().PauseGame();
    }
}
