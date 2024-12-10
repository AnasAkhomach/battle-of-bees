using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

    public enum EState
    {
        NORMAL, RUN, DIE, HIT
    }

    public Transform effectHit;
    public Transform effectScore;
    public float speed { get; set; }
    float speedDefault;
    float speedCurrent;
    public EState State { get; set; }
    GameObject playerObject;
    //GameObject sprite;
    public bool checkPos { get; set; }
    float z;
    float stateTime;
    float hp;
    float hpCurrent;
    HpScript hpScript;
    public Transform particle;
    GameObject anim;
    GameScreen gameScreen;
    Vector3 posCurent;
    Vector3 localScaleCurrent;
    void Awake()
    {
        posCurent = transform.position;
        if (PlayerPrefs.GetInt(SPath.LEVEL) > 0)
        {
            hp = 2;
            speedDefault = 1;
        }
        else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 4)
        {
            hp = 3;
            speedDefault = 2;
        }

        else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 8)
        {
            hp = 4;
            speedDefault = 3;
        }
        else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 12)
        {
            hp = 5;
            speedDefault = 5;
        }
        hpCurrent = hp;
        speedCurrent = speedDefault;
        z = transform.position.z;
        localScaleCurrent = transform.localScale;
        gameScreen = GameObject.Find("GameScreen").GetComponent<GameScreen>();
        playerObject = GameObject.Find("player");
        anim = transform.Find("anim").gameObject;
        hpScript = this.GetComponent<HpScript>();
        //    if (PlayerPrefs.GetInt(SPath.LEVEL) > 0)
        //    {
        //        hp = 2;
        //        speedDefault = 1;
        //    }
        //    else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 4)
        //    {
        //        hp = 3;
        //        speedDefault = 2;
        //    }

        //    else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 8)
        //    {
        //        hp = 4;
        //        speedDefault = 3;
        //    }
        //    else if (PlayerPrefs.GetInt(SPath.LEVEL) >= 12)
        //    {
        //        hp = 5;
        //        speedDefault = 5;
        //    }
        //    gameScreen = GameObject.Find("GameScreen").GetComponent<GameScreen>();
        //    speed = speedDefault;
        //    playerObject = GameObject.Find("player");
        //    anim = transform.FindChild("anim").gameObject;
        //    hpScript = this.GetComponent<HpScript>();
        //    hpScript.HP = this.hp;
    }

    void Start()
    {
        //if (gameScreen.State == GameScreen.GState.GAME_WIN || gameScreen.State == GameScreen.GState.GAME_LOSE)
        //    return;
        //setNormal();
        //z = transform.position.z;
        //checkPos = transform.position.x <= playerObject.transform.position.x ? true : false;
        //Vector3 scale = anim.transform.localScale;
        //if (!checkPos)
        //    anim.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        //else
        //    anim.transform.localScale = scale;
    }

    void OnEnable()
    {
        transform.position = posCurent;
        State = EState.NORMAL;
        hp = hpCurrent;
        speedDefault = speedCurrent;
        stateTime = 0;
        speed = speedDefault;
        hpScript.HP = this.hp;

        checkPos = transform.position.x <= playerObject.transform.position.x ? true : false;
        transform.localScale = localScaleCurrent;
    }

    float v2;
    void Update()
    {
        if (gameScreen.State == GameScreen.GState.GAME_WIN || gameScreen.State == GameScreen.GState.GAME_LOSE)
            return;

        stateTime += Time.deltaTime;
        switch (State)
        {
            case EState.NORMAL:
                transform.position += checkPos ? new Vector3(speed * Time.deltaTime, 0, 0) : new Vector3(-speed * Time.deltaTime, 0, 0);
                if (stateTime >= 1)
                    setRun();
                break;
            case EState.RUN:
                transform.position += checkPos ? new Vector3(speed * Time.deltaTime, 0, 0) : new Vector3(-speed * Time.deltaTime, 0, 0);
                float y = transform.position.y;
                if (y < playerObject.transform.position.y)
                    y += Time.deltaTime;
                if (y > playerObject.transform.position.y)
                    y -= Time.deltaTime;
                transform.position = new Vector3(transform.position.x, y, z);
                break;
            case EState.HIT:
                setRun();
                break;
            case EState.DIE:
                break;
        }

        if ((checkPos && transform.position.x > Camera.main.transform.position.x + 5) || (!checkPos && transform.position.x < Camera.main.transform.position.x - 5))
            gameObject.SetActive(false);
        if (hpScript.HP <= 0)
            setDie();

        if (gameScreen.State == GameScreen.GState.RUNNING)
        {
            v2 += 0.05f;
            transform.rotation = Quaternion.EulerAngles(new Vector3(0, 0, (Mathf.Sin(Time.time * speed2) * magnitude)));
        }
    }
    float magnitude = .3f; // cuong do
    float speed2 = 4;



    public void setNormal()
    {
        if (State != EState.DIE)
        {
            stateTime = 0;
            State = EState.NORMAL;
        }
    }

    public void setRun()
    {
        if (State != EState.DIE)
        {
            if (Random.Range(0, 3) == 0)
                speed = speedDefault * 2;
            else
                speed = speedDefault;
            stateTime = 0;
            State = EState.RUN;
        }
    }

    public void setDie()
    {
        if (State != EState.DIE)
        {
            //MyPref.Save_Count_OngBom(1);
            //int score = 5 + MyPref.Get_Bonus_OngBom();
            //MyPref.Save_Score_Current(score);
            //SPath.Score_current += score;
            //MyPref.Save_Bonus_OngBom(1);

            GameContains.Count_OngBom += 1;
            int score = 5 + GameContains.Count_Bonus_Bom;
            GameContains.Score += score;
            SPath.Score_current += score;
            GameContains.Count_Bonus_Bom += 1;

            stateTime = 0;

            Instantiate(particle, transform.position, Quaternion.identity);
            var abc = Instantiate(effectScore, Camera.main.WorldToViewportPoint(transform.position), Quaternion.identity) as Transform;
            abc.GetComponent<ScoreEffect>().SetValue("+" + score);
            gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(3);
            State = EState.DIE;
        }
    }

    public void setHit()
    {
        if (State != EState.HIT && State != EState.DIE)
        {
            stateTime = 0;
            State = EState.HIT;
        }
    }

    void addHp(float hp)
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
                Instantiate(effectHit, new Vector3(transform.position.x, transform.position.y, -10), Quaternion.identity);
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
