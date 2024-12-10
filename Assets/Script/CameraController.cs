using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public enum CState
    {
        VIBRATE, NORMAL
    }
    float stateTime;
    float sizeDefault;
    public float timeEnd
    {
        get;
        set;
    }

    public CState State
    {
        get;
        set;
    }
    // Use this for initialization
    void Start()
    {
        sizeDefault = 2.4f;
        setNormal();
    }

    // Update is called once per frame
    void Update()
    {
        stateTime += 0.5f;
        switch(State){
            case CState.NORMAL:
                Camera.main.orthographicSize = sizeDefault;
                break;
            case CState.VIBRATE:
                if (stateTime >= timeEnd)
                    setNormal();
                Camera.main.orthographicSize = (float)(sizeDefault - ((float)Mathf.Abs(Mathf.Sin(stateTime) * 0.1f)));
                break;
        }
    }

    public void setVibrate(float timeEnd)
    {
        this.timeEnd = timeEnd;
        if(State != CState.VIBRATE){
            stateTime = 0;
            State = CState.VIBRATE;
        }
    }

    public void setNormal()
    {
        stateTime = 0;
        State = CState.NORMAL;
    }
}
