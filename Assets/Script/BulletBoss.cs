using UnityEngine;
using System.Collections;

public class BulletBoss : MonoBehaviour
{

    public enum BBState
    {
        READY, READY2, FIRE
    }

    public BBState State { get; set; }
    public Transform hit;
    GameObject player;
    GameObject boss;
    BossController bossController;
    float stateTime;
    public float speed = 1;
    Vector3 target;
    Vector3 direction;

    float radius = 1.5f;
    Vector3 offset;
    float x, y;
    public int id { get; set; }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(SPath.TAG_PLAYER);
        boss = GameObject.FindGameObjectWithTag(SPath.TAG_BOSS);
        bossController = boss.GetComponent<BossController>();
        if (bossController.State == BossController.BState.ATTACK)
            setReady();
        if (bossController.State == BossController.BState.ATTACK2)
            setReady2();

    }

    float abc;
    float speedR = 0.06f;

    Vector3 p1, p2, result;
    float angle;
    void Update()
    {
        if (boss == null)
        {
            return;
        }
        p1 = transform.position;
        p2 = player.transform.position;
        angle = Mathf.Atan2(p1.y - p2.y, p1.x - p2.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));

        if (SPath.CHECK_BOSS == 1)
            Destroy(gameObject);
        stateTime += 0.04f;
        abc += speedR;
        switch (State)
        {
            case BBState.READY:
                offset = boss.transform.position;
                x = -(radius * Mathf.Cos(abc)) + offset.x;
                y = -(radius * Mathf.Sin(abc)) + offset.y;
                transform.position = new Vector3(x, y, offset.z);
                if (stateTime >= 4f)
                {
                    setFire();
                    target = player.transform.position;
                    direction = (target - transform.position).normalized;
                }
                break;
            case BBState.READY2:

                if (stateTime >= 8)
                {
                    target = player.transform.position;
                    direction = (target - transform.position).normalized;
                    setFire();
                }
                else
                {
                    switch (id)
                    {
                        case 3:
                            transform.position = new Vector3(boss.transform.position.x - .2f, boss.transform.position.y + .2f);
                            break;
                        case 2:
                            transform.position = new Vector3(boss.transform.position.x - .7f, boss.transform.position.y + .2f);
                            break;
                        case 1:
                            transform.position = new Vector3(boss.transform.position.x - 1.2f, boss.transform.position.y + .2f);
                            break;
                        case 6:
                            transform.position = new Vector3(boss.transform.position.x - .2f, boss.transform.position.y - .8f);
                            break;
                        case 5:
                            transform.position = new Vector3(boss.transform.position.x - .7f, boss.transform.position.y - .8f);
                            break;
                        case 4:
                            transform.position = new Vector3(boss.transform.position.x - 1.2f, boss.transform.position.y - .8f);
                            break;
                        default:
                            return;
                    }
                }
                break;
            case BBState.FIRE:
                transform.position += direction / 5;
                break;
        }

        if (transform.position.x <= Camera.main.transform.position.x - 5)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(SPath.TAG_PLAYER))
        {
            Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setReady()
    {
        stateTime = 0;
        State = BBState.READY;
    }

    public void setFire()
    {
        stateTime = 0;
        State = BBState.FIRE;
    }

    public void setReady2()
    {
        stateTime = 0;
        State = BBState.READY2;
    }
}
