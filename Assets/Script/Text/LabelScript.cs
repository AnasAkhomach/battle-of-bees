using UnityEngine;
using System.Collections;

public class LabelScript : MonoBehaviour
{

    GameObject obj;
    TimeText timeText;
    public int id;
    void Awake()
    {
        obj = GameObject.Find("GameScreen");
        timeText = obj.GetComponent<TimeText>();
    }

    void Update()
    {
        switch (id)
        {
            case 1:
                gameObject.GetComponent<dfLabel>().Text = "" + timeText.time;
                break;
            case 2:
                gameObject.GetComponent<dfLabel>().Text = "" + timeText.level;
                break;
            case 3:
                gameObject.GetComponent<dfLabel>().Text = "" + timeText.totalBullet;
                break;
        }

    }
}
