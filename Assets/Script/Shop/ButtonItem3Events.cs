using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonItem3Events : MonoBehaviour 
{
    public Transform tickCoin;
    public Transform tickRuby;
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        if (SPath.Check_Coin)
        {
            if (MyPref.GetCoin() >= SPath.ITEM_3)
            {
                GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(1);
                MyPref.addDan(3, SPath.TOTAL_BULLET_ITEM_3);
                MyPref.SaveCoin(-SPath.ITEM_3);
            }
            else
            {
                GameObject.FindGameObjectWithTag("chuyendoi").GetComponent<dfPanel>().IsVisible = true;

                //bool b = true;
                //SPath.Check_Coin = !b;
                //tickCoin.GetComponent<dfSprite>().IsVisible = !b;
                //tickRuby.GetComponent<dfSprite>().IsVisible = b;

                GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                //Dialog het coin
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(1);
            if (MyPref.GetRuby() >= 6)
            {
                MyPref.addDan(3, SPath.TOTAL_BULLET_ITEM_3);
                MyPref.SaveRuby(-6);
            }
            else
            {
                GameObject.FindGameObjectWithTag("naptien").GetComponent<dfPanel>().IsVisible = true;

                //bool b = false;
                //SPath.Check_Coin = !b;
                //tickCoin.GetComponent<dfSprite>().IsVisible = !b;
                //tickRuby.GetComponent<dfSprite>().IsVisible = b;
                //GameObject.FindGameObjectWithTag("stripShops").GetComponent<dfTabstrip>().SelectedIndex = 1;

                GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(6);
                //Dialog het ruby
            }
        }
	}

}
