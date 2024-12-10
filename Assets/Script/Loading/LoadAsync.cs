using UnityEngine;
using System.Collections;

public class LoadAsync : MonoBehaviour
{
    public static string levelName = "ChooseMap";
    public static int levelID = 3;
    public static bool isLoadById = false;

    private float timerLabel = 0;
    private bool isLoading = true;
    private string loadingLabel = "LOADING";
    private AsyncOperation async;
    public float percent { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (isLoadById == false)
            StartCoroutine(Load());
        else
            StartCoroutine(LoadLeveByID());
    }

    void Update()
    {
        //#if UNITY_EDITOR
        //if (Input.GetKeyUp (KeyCode.Mouse0)) {
        //    Time.timeScale = 1;
        //    Destroy (gameObject);
        //}
        //#endif

        //if (Input.GetKeyUp (KeyCode.Escape)) {
        //    Time.timeScale = 1;
        //    Destroy (gameObject);
        //}

        if (Input.touchCount > 0 && isLoading == false)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 20;
        if (isLoading == false)
        {
            Time.timeScale = 1;
        }
        else
        {
            GUI.skin.label.fontSize = 30;
            GUI.color = Color.white;
            //GUI.Label(new Rect(15, 430, 300, 100), "Loading " + async.progress * 100 + "%");
            percent = async.progress;
            if (async.progress >= 1)
                Destroy(gameObject);
        }
    }

    IEnumerator Load()
    {
        isLoading = true;
        async = Application.LoadLevelAsync(levelName);

        yield return async;
        isLoading = false;
        // Load xong
        // lamf gif thi lafm

        Destroy(gameObject);
        System.GC.Collect();
    }

    IEnumerator LoadLeveByID()
    {
        isLoading = true;
        async = Application.LoadLevelAsync(levelID);
        Debug.Log(async.progress);
        yield return async;
        isLoading = false;
        // Load xong
        // lamf gif thi lafm

        Time.timeScale = 0;
        System.GC.Collect();
    }
}
