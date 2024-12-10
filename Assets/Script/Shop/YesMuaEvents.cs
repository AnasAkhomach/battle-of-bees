using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YesMuaEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        GameObject.FindGameObjectWithTag("stripShops").GetComponent<dfTabstrip>().SelectedIndex = 1;
	}

}
