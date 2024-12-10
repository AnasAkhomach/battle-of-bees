using UnityEngine;
using System.Collections;

public class ClickOption : MonoBehaviour
{

    //dfTweenGroup dfGroup;
    //void Awake()
    //{
    //    dfGroup = GameObject.FindGameObjectWithTag(SPath.TAG_MAIN_OPTION).GetComponent<dfTweenGroup>();
    //}

    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        if (MenuScreen.State == MenuScreen.MState.MAIN_MENU)
            MenuScreen.State = MenuScreen.MState.OPTION;
        //for (int i = 0; i < dfGroup.Tweens.Count; i++)
        //{
        //    if (dfGroup.Tweens[i].TweenName.Equals("scaleIn"))
        //        if (!dfGroup.Tweens[i].IsPlaying)
        //        {
        //            MenuScreen.State = MenuScreen.MState.OPTION;
        //            dfGroup.Tweens[i].Play();
        //        }
        //}
    }
}
