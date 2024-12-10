using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyItem1Events : MonoBehaviour 
{
    public Transform tickCoin;
    public Transform tickRuby;

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		if(SPath.Check_Coin){
            if (MyPref.GetCoin() >= SPath.ITEM_1)
            {
                GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(1);
                MyPref.addDan(1,SPath.TOTAL_BULLET_ITEM_1);
                MyPref.SaveCoin(-SPath.ITEM_1);
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
            Debug.Log("s----------------------------------------------------d");
            if (MyPref.GetRuby() >= 2)
            {
                GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(1);
                MyPref.addDan(1, SPath.TOTAL_BULLET_ITEM_1);
                MyPref.SaveRuby(-2);
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
