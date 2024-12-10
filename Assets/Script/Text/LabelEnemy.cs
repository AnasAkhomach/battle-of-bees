using UnityEngine;
using System.Collections;

public class LabelEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.GetComponent<dfLabel>().Text = "" + SPath.COUNT_ONG_BOM;
        gameObject.GetComponent<dfLabel>().Text = "" + GameContains.Count_OngBom;
	}
}
