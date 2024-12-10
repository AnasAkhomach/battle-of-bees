using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btRetryEvents : MonoBehaviour 
{
    
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        //GoolgeAdmob.bannerView.Hide();
        MyPref.Reset_Component();
        MyPref.load();
        LoadAsync.levelName = SPath.S_GAMESCREEN;
        Application.LoadLevel(SPath.S_LOADING);
	}

}
