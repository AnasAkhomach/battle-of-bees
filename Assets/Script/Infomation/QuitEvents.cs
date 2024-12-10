using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuitEvents : MonoBehaviour 
{
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        if (MenuScreen.State == MenuScreen.MState.HELPER && GameObject.Find("Panel Help").GetComponent<dfPanel>().IsVisible)
        {
            GameObject.Find("Panel Help").GetComponent<dfPanel>().IsVisible = false; ;
            MenuScreen.State = MenuScreen.MState.MAIN_MENU;
        }
	}

	public void OnMouseDown( dfControl control, dfMouseEventArgs mouseEvent )
	{
		// Add event handler code here
		Debug.Log( "MouseDown" );
	}
}
