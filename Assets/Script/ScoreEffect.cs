using UnityEngine;
using System.Collections;

public class ScoreEffect : MonoBehaviour
{

    string str = ".";
    Vector3 posTarget;
    float stateTime;

    // Use this for initialization
    void Start()
    {
        stateTime = 0;
        posTarget = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
        iTween.MoveTo(gameObject, new Vector3(0.3f,0.9f), 2f);
        //iTween.ColorTo(gameObject, new Color(1, 1, 1, 0), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        stateTime += Time.deltaTime;
        GetComponent<GUIText>().text =  getValue();
        if (stateTime >= 1.8f)
            Destroy(gameObject);
    }

    public void SetValue(string value)
    {
        str = value;
    }
    public string getValue()
    {
        return str;
    }
}
