using UnityEngine;
using System.Collections;

public class Enemy3Controller : MonoBehaviour
{

    public enum E3State
    {
        NORMAL, DIE
    }

    public Transform effectHit;
    public Transform effectScore;
    float stateTime;
    public E3State State { get; set; }
    public Vector3 speed;
    public Transform particle;
    public bool checkPos { get; set; }
    GameObject anim;
    float hp = 2;
    Vector3 posCurrent;
    Vector3 scaleCurrent;
    void Awake()
    {
        posCurrent = transform.position;
        scaleCurrent = transform.localScale;
        //anim = transform.FindChild("anim").gameObject;
    }
    void Start()
    {
        //checkPos = transform.position.x <= Camera.main.transform.position.x ? true : false;
        //speed = checkPos ? speed : -speed;
        //anim.transform.localScale = checkPos ? anim.transform.localScale : new Vector3(-anim.transform.localScale.x, anim.transform.localScale.x, anim.transform.localScale.y);
    }

    void OnEnable()
    {
        State = E3State.NORMAL;
        stateTime = 0;
        hp = 2;
        transform.position = posCurrent;
        anim = transform.Find("anim").gameObject;
        checkPos = posCurrent.x <= Camera.main.transform.position.x ? true : false;
        speed = scaleCurrent.x == 1 ? speed : -speed;
        transform.localScale = scaleCurrent;
    }

    void Update()
    {
        stateTime += Time.deltaTime;
        switch (State)
        {
            case E3State.NORMAL:
                transform.Translate(speed * Time.deltaTime);
                if (hp <= 0)
                {
                    setDie();
                }
                if ((checkPos && transform.position.x >= Camera.main.transform.position.x + 5) || (!checkPos && transform.position.x <= Camera.main.transform.position.x - 5))
                    gameObject.SetActive(false);
                break;
            case E3State.DIE:
                break;
        }
    }

    public void setNormal()
    {
        if (State != E3State.DIE)
        {
            stateTime = 0;
            State = E3State.NORMAL;
        }
    }

    public void setDie()
    {
        if (State != E3State.DIE)
        {
            //GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(4);
            stateTime = 0;
            Instantiate(particle, transform.position, Quaternion.identity);
            var abc = Instantiate(effectScore, Camera.main.WorldToViewportPoint(transform.position), Quaternion.identity) as Transform;
            abc.GetComponent<ScoreEffect>().SetValue("-" + 5);

            if (GameContains.Score >= 5)
            {
                GameContains.Score -= 5;
                SPath.Score_current -= 5;
            }
            GameContains.Count_Bonus_OngChich = 0;
            GameContains.Count_Bonus_Bom = 0;
            //MyPref.Reset_Bonus_OngBom();
            //MyPref.Reset_Bonus_OngChich();

            State = E3State.DIE;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(SPath.TAG_BULLET))
        {
            var bullet = other.gameObject as GameObject;
            BulletController bullController = bullet.GetComponent<BulletController>();
            if (bullet != null)
            {
                Instantiate(effectHit, new Vector3(transform.position.x, transform.position.y, -10), Quaternion.identity);
                Destroy(bullet.gameObject);
                hp -= 1;
            }
        }
    }
}
