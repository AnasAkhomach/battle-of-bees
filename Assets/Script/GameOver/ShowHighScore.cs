using UnityEngine;
using System.Collections;

public class ShowHighScore : MonoBehaviour {

   
	void Update () {
        gameObject.GetComponent<dfLabel>().Text = MyPref.getScore().ToString();
	}
}
