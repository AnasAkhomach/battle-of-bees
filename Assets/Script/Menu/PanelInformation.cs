using UnityEngine;
using System.Collections;

public class PanelInformation : MonoBehaviour
{
    public void ShowGroup()
    {
        for (int i = 0; i < GetComponent<dfTweenGroup>().Tweens.Count; i++)
        {
            if (GetComponent<dfTweenGroup>().Tweens[i].TweenName.Equals("GroupIn") && !GetComponent<dfTweenGroup>().Tweens[i].IsPlaying)
            {
                GetComponent<dfTweenGroup>().Tweens[i].Play();
            }
        }
    }

   public  void HideGroup()
    {
        for (int i = 0; i < GetComponent<dfTweenGroup>().Tweens.Count; i++)
        {
            if (GetComponent<dfTweenGroup>().Tweens[i].TweenName.Equals("GroupOut") && !GetComponent<dfTweenGroup>().Tweens[i].IsPlaying)
            {
                GetComponent<dfTweenGroup>().Tweens[i].Play();
            }
        }
    }
}
