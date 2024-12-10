using UnityEngine;
using System.Collections;

public class FixCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(800, 480, true);
        if (GetComponentInChildren<Camera>()!=null)
        {
            GetComponentInChildren<Camera>().aspect = 800f / 480f;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
