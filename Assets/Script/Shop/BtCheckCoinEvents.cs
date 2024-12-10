using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BtCheckCoinEvents : MonoBehaviour
{

    void Start()
    {
        if (SPath.Check_Coin)
            transform.GetComponentInChildren<dfSprite>().IsVisible = true;
        else
            transform.GetComponentInChildren<dfSprite>().IsVisible = false;
    }
    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        if (!SPath.Check_Coin)
            SPath.Check_Coin = true;
    }

}
