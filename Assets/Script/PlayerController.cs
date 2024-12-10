using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public enum PState
    {
        NORMAL, FIRE, HIT, DIE
    }

    public Transform effectIHP, effectIBullet;

    public PState State { get; set; }
    float hp;
    HpScript hpScript;
    float stateTime;
    CameraController cameraController;
    BoxCollider2D boxColl;
    public Vector2 center { get; set; }
    GameObject gamescreen;

    GameObject joyStick;
    StateJoyStick stateJoyStick;

    SheildController sheildController;
    float rateDef = 0.5f;

    public bool AddBullet { get; set; }

    GameObject objectAudio;
    AudioController audioController;

    void Awake()
    {
        hp = 10;
        joyStick = GameObject.FindGameObjectWithTag("joystick");
        stateJoyStick = joyStick.GetComponent<StateJoyStick>();
        gamescreen = GameObject.Find("GameScreen");
        sheildController = this.GetComponent<SheildController>();
        hpScript = this.GetComponent<HpScript>();
        hpScript.HP = this.hp;
        cameraController = gamescreen.GetComponent<CameraController>();
        boxColl = this.GetComponent<BoxCollider2D>();
        AddBullet = false;
    }

    // Use this for initialization
    void Start()
    {
        objectAudio = GameObject.FindGameObjectWithTag("audio");
        audioController = objectAudio.GetComponent<AudioController>();
        setNormal();
    }

    float v, v2;
    float y, y2;
    float yDefualt;
    // Update is called once per frame
    void Update()
    {

        v += 0.02f;
        v2 += Time.deltaTime;
        center = boxColl.offset;
        stateTime += Time.deltaTime;
        switch (State)
        {
            case PState.NORMAL:

#if UNITY_EDITOR || UNITY_WEBPLAYER
                MyController();
#else
                if (stateJoyStick.isTouch)
                {
                    y = transform.position.y;
                    yDefualt = transform.position.y;
                }
                else
                {
                    y = yDefualt-(Mathf.Sin(Time.time * speed2) * magnitude);
                }
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                y2 = transform.position.x;
#endif
                break;
            case PState.FIRE:
                break;
            case PState.HIT:
                break;
            case PState.DIE:
                break;
        }

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, -3.5f , 3.5f),
          Mathf.Clamp(transform.position.y, -2f  , 2f ),
          transform.position.z
        );

        if (hpScript.HP <= 0)
            setDie();
    }

    float magnitude = 0.1f; // cuong do
    float speed2 = 2;

    void MyController()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h * Time.deltaTime * 3, v * Time.deltaTime * 3, 0), Space.World);

        if (h != 0 || v != 0)
        {
            y = transform.position.y;
            yDefualt = transform.position.y;
        }
        else if (h == 0 && v == 0)
        {
            y = yDefualt - (Mathf.Sin(Time.time * speed2) * magnitude);
        }
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        y2 = transform.position.x;
    }

    public void setNormal()
    {
        if (State != PState.DIE)
        {
            stateTime = 0;
            State = PState.NORMAL;
        }
    }

    public void setFire()
    {
        //if (State != PState.FIRE && State != PState.DIE)
        //{
        //    v2 = 0;
        //    stateTime = 0;
        //    State = PState.FIRE;
        //}
    }

    public void setHit()
    {
        if (State != PState.HIT && State != PState.DIE)
        {
            stateTime = 0;
            State = PState.HIT;
        }
    }

    public void setDie()
    {
        if (State != PState.DIE)
        {
            stateTime = 0;
            State = PState.DIE;
            gamescreen.GetComponent<GameScreen>().callGameLose();
            Destroy(gameObject);
        }
    }

    public void addHp(float hp)
    {
        hpScript.HP += hp;
    }

    void sheildControl()
    {
        switch (sheildController.State)
        {
            case SheildController.SHState.NORMAL:
                addHp(-1);
                break;
            case SheildController.SHState.PROTECT:
                addHp(0);
                break;
            case SheildController.SHState.DEFENSE:
                addHp(-1 * rateDef);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case SPath.TAG_ONGBOM:
                var ongbom = other.gameObject as GameObject;
                EnemyController enemy = ongbom.GetComponent<EnemyController>();
                if (ongbom != null)
                {
                    cameraController.setVibrate(2.5f);
                    enemy.setDie();
                    sheildControl();
                }
                break;
            case SPath.TAG_ONGCHICH:
                var ongchich = other.gameObject as GameObject;
                Enemy2Controller enemy2 = ongchich.GetComponent<Enemy2Controller>();
                if (ongchich != null)
                {
                    cameraController.setVibrate(2.5f);
                    enemy2.setDie();
                    sheildControl();
                }
                break;
            case SPath.TAG_ONGMAT:
                var ongmat = other.gameObject as GameObject;
                Enemy3Controller enemy3 = ongmat.GetComponent<Enemy3Controller>();
                if (ongmat != null)
                {
                    cameraController.setVibrate(2.5f);
                    enemy3.setDie();
                    sheildControl();
                }
                break;

            case SPath.TAG_BULLET_BOSS:
                var bullet = other.gameObject as GameObject;
                if (bullet != null)
                {

                    cameraController.setVibrate(1.5f);
                    sheildControl();
                    Destroy(bullet);
                }
                break;
            case SPath.TAG_ITEM_BULLET:
                var ibullet = other.gameObject as GameObject;
                if (ibullet != null && !AddBullet)
                {
                    audioController.playSound(1);
                    AddBullet = true;
                    Instantiate(effectIBullet, ibullet.transform.position, Quaternion.identity);
                    Destroy(ibullet);
                }
                break;
            case SPath.TAG_ITEM_HP:
                var iHp = other.gameObject as GameObject;
                if (iHp != null)
                {
                    audioController.playSound(1);
                    addHp(2);
                    Instantiate(effectIHP, iHp.transform.position, Quaternion.identity);
                    Destroy(iHp);
                }
                break;
        }
    }

    public void saveHP()
    {
        MyPref.setHP(hpScript.HP);
    }
}
