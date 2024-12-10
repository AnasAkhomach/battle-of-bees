using UnityEngine;
using System.Collections;

public class EffectOverlapItem : MonoBehaviour {

    public Vector3 scaleTarget = new Vector3(2.5f, 2.5f, 1);
	// Use this for initialization
	void Start () {
        iTween.ScaleTo(gameObject, scaleTarget, 1f);
        iTween.RotateTo(gameObject, new Vector3(0, 0, 90), 1);
        Destroy(gameObject,0.5f);
	}
}
