using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btMoreEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Zomegamelab");
	}
}
