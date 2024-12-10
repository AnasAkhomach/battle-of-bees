using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btNextLevel : MonoBehaviour
{

    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        //GoolgeAdmob.bannerView.Hide();
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        LoadAsync.levelName = SPath.S_GAMESCREEN;
        Application.LoadLevel(SPath.S_LOADING);
        
    }
}
