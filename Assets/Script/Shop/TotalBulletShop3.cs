using UnityEngine;
using System.Collections;

public class TotalBulletShop3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<dfLabel>().Text = MyPref.getDan(3).ToString();
	}
}
