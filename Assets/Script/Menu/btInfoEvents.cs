using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btInfoEvents : MonoBehaviour
{

    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        if (MenuScreen.State == MenuScreen.MState.MAIN_MENU)
        {
            MenuScreen.State = MenuScreen.MState.INFOR;
            transform.parent.Find("PanelInformation").GetComponent<PanelInformation>().ShowGroup();
        }
    }
}
