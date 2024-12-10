using UnityEngine;
using System.Collections;

public class ShowScoreCurrent : MonoBehaviour
{
    void Update()
    {
        //gameObject.GetComponent<dfLabel>().Text = SPath.COUNT_SCORE.ToString();
        gameObject.GetComponent<dfLabel>().Text = MyPref.Get_Score_Current().ToString();
    }
}
