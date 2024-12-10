using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    GameObject line;
    GameObject obj;
    HpScript hp;
    float maxHp;
    public bool player;
    // Use this for initialization
    void Start()
    {
        if(player){
            line = transform.Find("Thanh Mau").gameObject;
            obj = GameObject.FindGameObjectWithTag(SPath.TAG_PLAYER);
            hp = obj.transform.GetComponent<HpScript>();
            maxHp = hp.HP;
        }
        else
        {
            line = transform.Find("thanhmau1a").gameObject;
            hp = transform.parent.GetComponent<HpScript>();
            maxHp = hp.HP;
        }
        line.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.HP >= maxHp)
            hp.HP = maxHp;
        float rate = hp.HP / maxHp;
        line.transform.localScale = new Vector3(rate, 1, 1);
    }
}
