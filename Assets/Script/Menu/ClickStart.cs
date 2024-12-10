using UnityEngine;
using System.Collections;

public class ClickStart : MonoBehaviour
{

    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        LoadAsync.levelName = SPath.S_GAMESCREEN;
        Application.LoadLevel(SPath.S_LOADING);
    }
}
