using UnityEngine;
using System.Collections;

public class FacebookShare : MonoBehaviour
{
    public static string GAME_NAME = "The Battle of Bee";
    public static string APP_ID = "835692639815828";
    //
    public static AndroidJavaClass facebook;

    void Start()
    {
       // init();
    }

    static AndroidJavaObject getUnityPlayerObject()
    {
        AndroidJavaClass parentClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activityObject = parentClass.GetStatic<AndroidJavaObject>("currentActivity");

        return activityObject;
    }

    void init()
    {
        prepareFacebook();
        facebook.CallStatic("initApp", GAME_NAME, APP_ID, this.gameObject.name);
    }

    static void prepareFacebook()
    {
        //if (facebook == null)
        //{
        //    //facebook = new AndroidJavaClass("vn.dev.util.lib.Facebook.FacebookController");
        //}
    }

    void OnFacebookShareSuccess(string mes)
    {
        //Share successful event
    }

    public static void postLinkToFacebook(string description)
    {
        prepareFacebook();
        facebook.CallStatic("postLinkToFacebook", getUnityPlayerObject(), description);
    }
}
