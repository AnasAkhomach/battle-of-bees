using UnityEngine;
using System.Collections;

public class Ruby : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<dfLabel>().Text = MyPref.GetRuby().ToString();
    }
}
