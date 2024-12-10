using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{

    GameObject bg1, bg2;

    void Awake()
    {
        bg1 = transform.Find("bg").gameObject;
        bg2 = transform.Find("bg2").gameObject;
        int rand = Random.Range(0, 2);
        if (rand == 0)
            setBG1();
        else
            setBG2();
    }

    void setBG1()
    {
        bg1.SetActive(true);
        bg2.SetActive(false);
    }

    void setBG2()
    {
        bg1.SetActive(false);
        bg2.SetActive(true);
    }
    
}
