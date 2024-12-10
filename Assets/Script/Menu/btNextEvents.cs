using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btNextEvents : MonoBehaviour
{
    dfTweenGroup dfGroup;
    void Start()
    {
        dfGroup = GameObject.FindGameObjectWithTag(SPath.TAG_MAIN_OPTION).GetComponent<dfTweenGroup>();
    }
    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        if (MenuScreen.State == MenuScreen.MState.OPTION)
            MenuScreen.State = MenuScreen.MState.MAIN_MENU;

        //for (int i = 0; i < dfGroup.Tweens.Count; i++)
        //{
        //    if (dfGroup.Tweens[i].TweenName.Equals("scaleOut"))
        //        if (!dfGroup.Tweens[i].IsPlaying)
        //            dfGroup.Tweens[i].Play();
        //}
    }

    public void HideOption()
    {

    }
}
