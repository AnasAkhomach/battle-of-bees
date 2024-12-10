using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{

    public enum BType
    {
        TYPE_1, TYPE_2, TYPE_3,TYPE_4
    }

    public BType Type { get; set; }
    float speed = 10;
    public float[] a { get; set; }
    public float angle { get; set; }
    public float alpha { get; set; }

    GameObject bullet1, bullet2;

    public Vector2 Target
    {
        get;
        set;
    }
    Vector2 myvt3;
    float stateTime, stateTime2;

    void Awake()
    {
        bullet1 = transform.Find("bullet").gameObject;
        bullet2 = transform.Find("bullet2").gameObject;
        bullet1.SetActive(true);
        bullet2.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 2f);
        myvt3 = transform.position;
        switch (MyPref.getGun())
        {
            case 1:
                Type = BType.TYPE_1;
                break;
            case 2:
                Type = BType.TYPE_2;
                break;
            case 3:
                Type = BType.TYPE_3;
                break;
            case 4:
                speed = 7;
                transform.localScale = new Vector3(.3f,.3f);
                Type = BType.TYPE_4;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (Type)
        {
            case BType.TYPE_1:
            case BType.TYPE_2:
            case BType.TYPE_4:
                //Vector3 vt3 = (Target - myvt3).normalized;
                Vector3 vt3 = new Vector3(Mathf.Cos(alpha * Mathf.Deg2Rad), Mathf.Sin(alpha * Mathf.Deg2Rad));
                transform.position += (vt3 * Time.deltaTime * speed);
                break;
            case BType.TYPE_3:
                stateTime += Time.deltaTime;
                stateTime2 += Time.deltaTime;
                if (stateTime < .3f)
                {
                    Vector3 direction = (Target - myvt3).normalized;
                    transform.position += (direction * Time.deltaTime * (speed / 2));
                }
                if (stateTime > .6f)
                {              
                    if (angle >= 0 && angle <= 90 || angle <= 360 && angle >= 270)
                    {
                        bullet2.SetActive(true);
                        bullet2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        transform.position += (new Vector3(speed * Time.deltaTime, 0, 0));
                    }
                    else
                    {
                        bullet1.SetActive(false);
                        bullet2.SetActive(true);
                        bullet2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        transform.position += (new Vector3(-speed * Time.deltaTime, 0, 0));
                    }
                }
                break;
        }
    }
}
