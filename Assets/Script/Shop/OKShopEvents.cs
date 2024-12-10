using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OKShopEvents : MonoBehaviour
{
    public Transform shop;
    public List<Transform> label;
    public bool b;
    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        if (b)
        {
            switch (PlayerPrefs.GetInt(SPath.LEVEL))
            {
                case 2:
                    if (!label[0].GetComponent<dfLabel>().IsVisible)
                    {
                        label[0].GetComponent<dfLabel>().IsVisible = true;
                        label[0].GetComponent<TextEffectWarning>().ShowText();
                    }
                    break;
                case 3:
                    if (!label[1].GetComponent<dfLabel>().IsVisible)
                    {
                        label[1].GetComponent<dfLabel>().IsVisible = true;
                        label[1].GetComponent<TextEffectWarning>().ShowText();
                    }
                    break;
                case 4:
                    if (!label[2].GetComponent<dfLabel>().IsVisible)
                    {
                        label[2].GetComponent<dfLabel>().IsVisible = true;
                        label[2].GetComponent<TextEffectWarning>().ShowText();
                    }
                    break;
                case 5:
                    if (!label[3].GetComponent<dfLabel>().IsVisible)
                    {
                        label[3].GetComponent<dfLabel>().IsVisible = true;
                        label[3].GetComponent<TextEffectWarning>().ShowText();
                    }
                    break;
            }
            Destroy(shop.gameObject);
        }
    }
}
