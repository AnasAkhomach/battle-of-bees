using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btHelperEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        if (MenuScreen.State == MenuScreen.MState.MAIN_MENU && !GameObject.Find("Panel Help").GetComponent<dfPanel>().IsVisible)
        {
            GameObject.Find("Panel Help").GetComponent<dfPanel>().IsVisible = true;
            MenuScreen.State = MenuScreen.MState.HELPER;
        }
	}
}
