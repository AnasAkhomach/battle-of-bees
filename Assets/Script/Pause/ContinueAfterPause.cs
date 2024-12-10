using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContinueAfterPause : MonoBehaviour
{
    GameScreen gameScreen;
    void Start()
    {
        gameScreen = GameObject.Find("GameScreen").GetComponent<GameScreen>();
        transform.parent.GetComponent<dfTweenGroup>();
    }
    public void OnMouseDown(dfControl control, dfMouseEventArgs mouseEvent)
    {

    }
    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        if (gameScreen.State == GameScreen.GState.RUNNING || Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Menu))
            return;
        //GameObject.Find("GameScreen").GetComponent<AdGoogle>().bannerView.Hide();
        //GoolgeAdmob.bannerView.Hide();
        gameScreen.State = GameScreen.GState.RUNNING;

    }
}
