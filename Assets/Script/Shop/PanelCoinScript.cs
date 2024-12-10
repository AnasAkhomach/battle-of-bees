using UnityEngine;
using System.Collections;

public class PanelCoinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!SPath.Check_Coin)
            gameObject.GetComponent<dfPanel>().IsVisible = false;
        else
            gameObject.GetComponent<dfPanel>().IsVisible = true;
	}
}
