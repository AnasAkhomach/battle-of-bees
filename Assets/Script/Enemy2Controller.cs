using UnityEngine;
using System.Collections;

public class Enemy2Controller : MonoBehaviour
{

    public enum E2State
    {
        NORMAL, READY ,RUN, DIE, HIT
    }

    public Transform effectHit;
    public Transform effectScore;
    public GameObject player;
    public float speed { get; set; }
    float speedDefault = 2;
    public E2State State { get; set; }
    float stateTime, stateTime2;
    int step;
    Vector3 posCurrent;
    float yTarget;
    MyBezier bezier;
    Vector2[] posBezier;
    bool checkPos;
    float hp;
    HpScript hpScript;
    public Transform particle;
    GameObject anim;

    Vector3 direction;
    Vector3 target;

    void Awake()
    {
        if (PlayerPrefs.GetInt(SPath.LEVEL) > 0)
        {
            hp = 2;
            speedDefault = 2;
        }
        else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 5)
        {
            hp = 3;
            speedDefault = 3;
        }
        else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 8)
        {
            hp = 5;
            speedDefault = 5;
        }
        else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 12)
        {
            hp = 6;
            speedDefault = 6;
        }
        anim = transform.Find("anim").gameObject;
        speed = speedDefault;
        setNormal();
        player = GameObject.Find("player");
        hpScript = this.GetComponent<HpScript>();
        hpScript.HP = this.hp;
    }
    void Start()
    {
        yTarget = Random.Range(-2, 2);
        posCurrent = transform.position;

        checkPos = posCurrent.x < Camera.main.transform.position.x ? true : false;
        posBezier = new Vector2[3];
        if (checkPos)
        {
            posBezier[0] = new Vector2(-3, 3);
            posBezier[1] = new Vector2(-4.5f, 1.2f);
            posBezier[2] = new Vector2(-3f, yTarget);
        }
        else
        {
            posBezier[0] = new Vector2(3, 3);
            posBezier[1] = new Vector2(4.5f, 1.2f);
            posBezier[2] = new Vector2(3f, yTarget);
        }

        Vector3 scale = transform.localScale;
        if (!checkPos)
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        else
            transform.localScale = scale;
        bezier = new MyBezier(posBezier);
    }

    Vector3 current;
    // Update is called once per frame
    void Update()
    {
        stateTime += Time.deltaTime;
        stateTime2 += Time.deltaTime;
        switch (State)
        {
            case E2State.NORMAL:
                bezier.update(stateTime / 2);
                transform.position = new Vector3(bezier.Position.x, bezier.Position.y, posCurrent.z);
                if (stateTime >= 2f)
                    stateTime = 2;

                if (stateTime2 >= 2.5f)
                {
                    setReady();
                }
                break;
            case E2State.READY:
                if (checkPos)
                {
                    transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
                }
                else
                {
                    transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
                }
                if(stateTime>0.5f){
                    setRun();
                }
                break;
            case E2State.RUN:
                target = player.transform.position;
                direction = (target - transform.position).normalized;
                //transform.position += checkPos ? new Vector3(10 * Time.deltaTime, 0, 0) : new Vector3(-10 * Time.deltaTime, 0, 0);
                //float y = transform.position.y;
                //if (y < player.transform.position.y)
                //    y += Time.deltaTime;
                //if (y > player.transform.position.y)
                //    y -= Time.deltaTime;
                //transform.position = new Vector3(transform.position.x, y, posCurrent.z);

                if (checkPos)
                {
                    if (transform.position.x >= (player.transform.position.x - 1))
                    {
                        //transform.position = Vector3.Slerp(transform.position, new Vector3(Camera.main.transform.position.x+5, 0, transform.position.z), Time.deltaTime * 2);
                        transform.position += new Vector3(Time.deltaTime * 7, 0, 0);
                        float y = transform.position.y;
                        if (y < player.transform.position.y)
                            y += Time.deltaTime;
                        if (y > player.transform.position.y)
                            y -= Time.deltaTime;
                        transform.position = new Vector3(transform.position.x, y, posCurrent.z);
                    }
                    else
                    {
                        transform.position += direction * Time.deltaTime * 6;
                    }
                }
                else
                {
                    if (transform.position.x <= (player.transform.position.x + 1))
                    {
                        transform.position -= new Vector3(Time.deltaTime * 7, 0, 0);
                        float y = transform.position.y;
                        if (y < player.transform.position.y)
                            y += Time.deltaTime;
                        if (y > player.transform.position.y)
                            y -= Time.deltaTime;
                        transform.position = new Vector3(transform.position.x, y, posCurrent.z);
                    }
                    else
                    {
                        transform.position += direction * Time.deltaTime * 6;
                    }
                }

                break;
            case E2State.HIT:
                break;
            case E2State.DIE:
                break;
        }

        if ((checkPos && transform.position.x >= Camera.main.transform.position.x + 5) || (!checkPos && transform.position.x <= Camera.main.transform.position.x - 5))
            Destroy(gameObject);
        if (hpScript.HP <= 0)
            setDie();
    }

    void moveToParabol(Vector2 form, Vector2 to)
    {

    }

    public void setNormal()
    {
        if (State != E2State.DIE)
        {
            stateTime = 0;
            stateTime2 = 0;
            State = E2State.NORMAL;
        }
    }

    public void setRun()
    {
        if (State != E2State.DIE)
        {
            stateTime = 0;
            State = E2State.RUN;
        }
    }

    public void setReady()
    {
        if(State != E2State.DIE){
            stateTime = 0;
            State = E2State.READY;
        }
    }

    public void setDie()
    {
        if (State != E2State.DIE)
        {
           
            //MyPref.Save_Count_OngChich(1);
            //int score = 5 + MyPref.Get_Bonus_OngChich();
            //MyPref.Save_Score_Current(score);
            //SPath.Score_current += score;
            //MyPref.Save_Bonus_OngChich(1);

            GameContains.Count_OngChich += 1;
            int score = 5 + GameContains.Count_Bonus_OngChich;
            GameContains.Score += score;
            SPath.Score_current += score;
            GameContains.Count_Bonus_OngChich += 1;


            stateTime = 0;
            State = E2State.DIE;
            var abc = Instantiate(effectScore, Camera.main.WorldToViewportPoint(transform.position), Quaternion.identity) as Transform;
            abc.GetComponent<ScoreEffect>().SetValue("+" + score);
            Instantiate(particle, new Vector3(transform.position.x, transform.position.y,-3), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setHit()
    {
        //if (State != E2State.DIE && State != E2State.HIT)
        //{
        //    stateTime = 0;
        //    State = E2State.HIT;
        //}
    }

    public void addHp(float hp)
    {
        hpScript.HP += hp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(SPath.TAG_BULLET))
        {
            var bullet = other.gameObject as GameObject;
            BulletController bullController = bullet.GetComponent<BulletController>();

            if (bullet != null)
            {
                Instantiate(effectHit, new Vector3(transform.position.x, transform.position.y + 0.5f, -10), Quaternion.identity);
                switch (bullController.Type)
                {
                    case BulletController.BType.TYPE_1:
                        addHp(-0.5f);
                        setHit();
                        Destroy(bullet);
                        break;
                    case BulletController.BType.TYPE_2:
                    case BulletController.BType.TYPE_4:
                        addHp(-1);
                        setHit();
                        Destroy(bullet);
                        break;
                    case BulletController.BType.TYPE_3:
                        setDie();
                        Destroy(bullet);
                        break;
                }
            }
        }
    }
}
