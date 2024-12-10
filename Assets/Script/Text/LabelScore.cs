using UnityEngine;
using System.Collections;

public class LabelScore : MonoBehaviour {
	void Update () {
        //gameObject.GetComponent<dfLabel>().Text = "" + SPath.COUNT_SCORE;
        gameObject.GetComponent<dfLabel>().Text = "" + GameContains.Score;
	}
}
