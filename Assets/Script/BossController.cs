using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{

    public enum BState
    {
        READY, MOVE, ATTACK, ATTACK2, DIE
    }

    public Vector2 speed;
    float stateTime, timeAttack;
    const float speedAttack = 0.2f;
    GameObject playerObject;
    GameObject normal;
    GameObject fire;
    public Transform bulletBoss;
    int count;
    float z;

    float hp;
    HpScript hpScript;
    public Transform particle;
    public Transform hit;

    public BState State
    {
        get;
        set;
    }

    void Awake()
    {
        if (PlayerPrefs.GetInt(SPath.LEVEL) >= 5)
            hp = 80 + 10 * (PlayerPrefs.GetInt(SPath.LEVEL) - 5);
        playerObject = GameObject.FindGameObjectWithTag(SPath.TAG_PLAYER);
        normal = transform.Find("normal").gameObject;
        fire = transform.Find("fire").gameObject;
        normal.SetActive(false);
        fire.SetActive(false);
        z = transform.position.z;
        hpScript = this.GetComponent<HpScript>();
        hpScript.HP = hp;
        randAttack = Random.Range(0, 10);
    }

    // Use this for initialization
    void Start()
    {
        setReady();
    }

    int randAttack;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("gamescreen").GetComponent<GameScreen>().State != GameScreen.GState.RUNNING)
            return;
        stateTime += Time.deltaTime;
        timeAttack += Time.deltaTime;
        Vector3 posCurrent = transform.position - playerObject.transform.position;
        float angle = Mathf.Atan2(posCurrent.y, posCurrent.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        switch (State)
        {
            case BState.READY:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(2, playerObject.transform.position.y, z), Time.deltaTime);
                if (transform.position.x <= 3)
                    setMove();
                break;
            case BState.MOVE:
                float y = transform.position.y;
                if (y < playerObject.transform.position.y)
                    y += Time.deltaTime * 1.5f;
                if (y > playerObject.transform.position.y)
                    y -= Time.deltaTime * 1.5f;
                transform.position = new Vector3(transform.position.x, y, z);
                if (stateTime > 3f)
                {
                    if (randAttack >=7)
                        setAttack2();
                    else
                        setAttack();
                    randAttack = Random.Range(0, 10);
                }

                break;
            case BState.ATTACK2:
                if (timeAttack >= speedAttack && count == 0)
                {
                    timeAttack = 0;
                    count = 1;
                    var bl = Instantiate(bulletBoss, transform.position, Quaternion.identity) as Transform;
                    BulletBoss bb = bl.GetComponent<BulletBoss>();
                    bb.id = 1;

                }
                if (timeAttack >= speedAttack && count == 1)
                {
                    count = 2;
                    timeAttack = 0;
                    var bl = Instantiate(bulletBoss, transform.position, Quaternion.identity) as Transform;
                    BulletBoss bb = bl.GetComponent<BulletBoss>();
                    bb.id = 2;
                }
                if (timeAttack >= speedAttack && count == 2)
                {
                    count = 3;
                    timeAttack = 0;
                    var bl = Instantiate(bulletBoss, transform.position, Quaternion.identity) as Transform;
                    BulletBoss bb = bl.GetComponent<BulletBoss>();
                    bb.id = 3;
                }
                if (timeAttack >= speedAttack && count == 3)
                {
                    count = 4;
                    timeAttack = 0;
                    var bl = Instantiate(bulletBoss, transform.position, Quaternion.identity) as Transform;
                    BulletBoss bb = bl.GetComponent<BulletBoss>();
                    bb.id = 4;
                }
                if (timeAttack >= speedAttack && count == 4)
                {

                    count = 5;
                    timeAttack = 0;
                    var bl = Instantiate(bulletBoss, transform.position, Quaternion.identity) as Transform;
                    BulletBoss bb = bl.GetComponent<BulletBoss>();
                    bb.id = 5;
                }
                if (timeAttack >= speedAttack && count == 5)
                {
                    count = 6;
                    timeAttack = 0;
                    var bl = Instantiate(bulletBoss, transform.position, Quaternion.identity) as Transform;
                    BulletBoss bb = bl.GetComponent<BulletBoss>();
                    bb.id = 6;
                    setMove();
                }

                break;
            case BState.ATTACK:
                if (timeAttack >= speedAttack && count == 0)
                {
                    Instantiate(bulletBoss, transform.position, Quaternion.identity);
                    count = 1;
                    timeAttack = 0;
                }
                if (timeAttack >= speedAttack && count == 1)
                {
                    Instantiate(bulletBoss, transform.position, Quaternion.identity);
                    count = 2;
                    timeAttack = 0;
                }
                if (timeAttack >= speedAttack && count == 2)
                {
                    Instantiate(bulletBoss, transform.position, Quaternion.identity);
                    count = 3;
                    timeAttack = 0;

                }
                if (timeAttack >= speedAttack && count == 3)
                {
                    Instantiate(bulletBoss, transform.position, Quaternion.identity);
                    count = 4;
                    timeAttack = 0;
                }
                if (timeAttack >= speedAttack && count == 4)
                {
                    Instantiate(bulletBoss, transform.position, Quaternion.identity);
                    count = 5;
                    timeAttack = 0;
                }
                if (timeAttack >= speedAttack && count == 5)
                {
                    Instantiate(bulletBoss, transform.position, Quaternion.identity);
                    count = 6;
                    timeAttack = 0;
                }
                if (timeAttack >= speedAttack && count == 6)
                {
                    Instantiate(bulletBoss, transform.position, Quaternion.identity);
                    count = 7;
                    timeAttack = 0;
                }
                if (count == 7)
                    setMove();
                break;
            case BState.DIE:
                break;
        }

        if (hpScript.HP <= 0)
            setDie();
    }

    public void setReady()
    {
        if (State != BState.DIE)
        {
            normal.SetActive(true);
            fire.SetActive(false);
            stateTime = 0;
            State = BState.READY;
        }
    }

    public void setMove()
    {
        if (State != BState.DIE)
        {

            normal.SetActive(true);
            fire.SetActive(false);
            stateTime = 0;
            count = 0;
            State = BState.MOVE;
        }
    }

    public void setAttack()
    {
        if (State != BState.DIE)
        {
            normal.SetActive(false);
            fire.SetActive(true);
            stateTime = 0;
            State = BState.ATTACK;
        }
    }

    public void setAttack2()
    {
        if (State != BState.DIE)
        {
            stateTime = 0;
            State = BState.ATTACK2;
        }
    }
    public void setDie()
    {
        if (State != BState.DIE)
        {
            //SPath.CHECK_BOSS = 1;
            Instantiate(particle, transform.position, Quaternion.identity);
            stateTime = 0;
            State = BState.DIE;
            MyPref.SaveRuby(1);
            Destroy(gameObject);
        }
    }

    public void addHp(float hp)
    {

        hpScript.HP += hp;
        Debug.Log("hit" + hpScript.HP);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //var bullet = other.transform.Find("bullet") as Transform;
        //if (bullet != null && State != BState.DIE)
        //{
        //    Instantiate(hit, transform.position, Quaternion.identity);
        //    addHp(-1);
        //    Destroy(bullet.gameObject);
        //}
        if (!other.tag.Equals(SPath.TAG_BULLET))
            return;
        var bullet = other.gameObject as GameObject;

        if (bullet != null)
        {
            BulletController bullController = bullet.GetComponent<BulletController>();
            Instantiate(hit, transform.position, Quaternion.identity);
            switch (bullController.Type)
            {
                case BulletController.BType.TYPE_1:
                    addHp(-0.5f);
                    Destroy(bullet);
                    break;
                case BulletController.BType.TYPE_2:
                case BulletController.BType.TYPE_4:
                    addHp(-1);
                    Destroy(bullet);
                    break;
                case BulletController.BType.TYPE_3:
                    addHp(-2);
                    Destroy(bullet);
                    break;
                default:
                    Destroy(bullet);
                    break;
            }
        }
    }
}
