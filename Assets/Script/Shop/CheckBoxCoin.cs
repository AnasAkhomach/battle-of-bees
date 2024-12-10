using UnityEngine;
using System.Collections;

public class CheckBoxCoin : MonoBehaviour {

    public bool Coin { get; set; }
    public bool Coin2 { get; set; }
	// Use this for initialization
	void Start () {
        //Coin = !Coin2;
	}

    public void SetCoin()
    {
        Coin = true;
        Coin2 = false;
    }
    public void SetCoin2()
    {
        Coin2 = true;
        Coin = false;
    }

    // Update is called once per frame
    void Update() {
        
	}
}
