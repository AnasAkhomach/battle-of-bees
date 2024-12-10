using UnityEngine;
using System.Collections;

public class LabelHetDan : MonoBehaviour
{
    public enum LState
    {
        SHOW, HIDE
    }
    public LState State { get; set; }
    float stateTime;
	// Use this for initialization
	void Start () {
        State = LState.HIDE; 
	}
	// Update is called once per frame
	void Update () {
        stateTime += Time.deltaTime;
        switch(State){
            case LState.SHOW:
                GetComponent<dfLabel>().IsVisible = true;
                if(stateTime>=1){
                    hide();
                }
                break;
            case LState.HIDE:
                GetComponent<dfLabel>().IsVisible = false;
                break;
        }
    }

    public void show()
    {
        stateTime = 0;
        State = LState.SHOW;
    }
    public void hide()
    {
        stateTime = 0;
        State = LState.HIDE;
    }
}
