using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btShareScoreEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        // show leaderboard UI
        Social.ShowLeaderboardUI();
	}
}
