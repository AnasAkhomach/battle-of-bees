using UnityEngine;
using System.Collections;

public class BgDark : MonoBehaviour
{
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];
            if (touch.phase == TouchPhase.Began)
            {
                if (MenuScreen.State == MenuScreen.MState.INFOR)
                {
                    transform.parent.Find("PanelInformation").GetComponent<PanelInformation>().HideGroup();
                    MenuScreen.State = MenuScreen.MState.MAIN_MENU;
                }
            }

        }

    }
}
