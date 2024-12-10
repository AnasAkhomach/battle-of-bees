using UnityEngine;
using System.Collections;

public class PerCentLoading : MonoBehaviour
{

    private GameObject line;
    private GameObject ong;
    private LoadAsync target;
    void Start()
    {
        line = transform.Find("line").gameObject;
        ong = transform.Find("ong").gameObject;
        target = GameObject.Find("SceneLoader").GetComponent<LoadAsync>();
    }

    void Update() 
    {
        line.transform.localScale = new Vector3(target.percent, 1, 1);
        ong.transform.position = new Vector3(((target.percent * 7.4f)-3f),-1.7f, 0);
    }
}
