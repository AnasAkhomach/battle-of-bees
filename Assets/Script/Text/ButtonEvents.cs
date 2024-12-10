using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonEvents : MonoBehaviour
{

    GameScreen gameScreen;
    void Start()
    {
        gameScreen = GameObject.Find("GameScreen").GetComponent<GameScreen>();
        if (gameScreen.State == GameScreen.GState.PAUSE || gameScreen.State == GameScreen.GState.GAME_WIN || gameScreen.State == GameScreen.GState.GAME_LOSE)
            return;
    }
    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {

        GameObject.FindGameObjectWithTag("audio").GetComponent<AudioController>();
        PauseGame();
    }

    public void PauseGame()
    {
        if (gameScreen.State == GameScreen.GState.RUNNING)
        {
            //GoolgeAdmob.bannerView.Show();
            //GameObject.Find("GameScreen").GetComponent<AdGoogle>().bannerView.Show();
            GameObject.Find("PanelPause").GetComponent<dfPanel>().IsVisible = true;
            GameObject.Find("PanelPause").GetComponent<dfTweenGroup>().Play();
            gameScreen.State = GameScreen.GState.PAUSE;
        }
    }
}
