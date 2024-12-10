using UnityEngine;
using System.Collections;

public class SheildController : MonoBehaviour
{

    public enum SHState
    {
        NORMAL, PROTECT, DEFENSE
    }

    public Transform effectIProtect, effectIDef;

    float stateTime;
    public SHState State { get; set; }
    GameObject protect;
    GameObject mu;
    const int timeProtect = 8;
    const float timeDefBonus = 3f;
    TimeItemDef timeItemDef;
    GameObject objectAudio;
    AudioController audioController;

    void Awake()
    {
        timeItemDef = this.GetComponent<TimeItemDef>();
        protect = transform.Find("Protect").gameObject;
        protect.SetActive(false);
        mu = transform.Find("mu").gameObject;
        mu.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        objectAudio = GameObject.FindGameObjectWithTag("audio");
        audioController = objectAudio.GetComponent<AudioController>();
        setNormal();
    }

    // Update is called once per frame
    void Update()
    {
        stateTime += Time.deltaTime;

        switch (State)
        {
            case SHState.NORMAL:
                break;
            case SHState.PROTECT:
                if (stateTime > timeProtect)
                {
                    setNormal();
                }
                break;
            case SHState.DEFENSE:
                timeItemDef.time -= Time.deltaTime/2;
                if (timeItemDef.time <= 0)
                {
                    setNormal();
                    timeItemDef.time = 0;
                }
                break;
        }
    }

    public void addTimeDef()
    {
        timeItemDef.time += timeDefBonus;
        if (timeItemDef.time >= SPath.TIME_DEFENSE_MAX)
            timeItemDef.time = SPath.TIME_DEFENSE_MAX;
    }

    public void setNormal()
    {
        stateTime = 0;
        protect.SetActive(false);
        mu.SetActive(false);
        State = SHState.NORMAL;
    }

    public void setProtect()
    {
        if (protect != null)
            protect.SetActive(true);
        stateTime = 0;
        State = SHState.PROTECT;
    }

    public void setDefense()
    {
        if (mu != null)
            mu.SetActive(true);
        stateTime = 0;
        State = SHState.DEFENSE;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case SPath.TAG_ITEM_PROTECT:
                var iprotect = other.gameObject as GameObject;
                if (iprotect != null)
                {
                    if (State != SHState.PROTECT && State != SHState.DEFENSE)
                    {
                        audioController.playSound(1);
                        setProtect();
                        Instantiate(effectIProtect,iprotect.transform.position,Quaternion.identity);
                        Destroy(iprotect);
                    }
                }
                break;
            case SPath.TAG_ITEM_DEFENSE:
                var idef = other.gameObject as GameObject;
                if (State != SHState.PROTECT)
                {
                    if (idef != null)
                    {
                        audioController.playSound(1);
                        setDefense();
                        addTimeDef();
                        Instantiate(effectIDef, idef.transform.position, Quaternion.identity);
                        Destroy(idef);
                    }
                }
                break;
        }
    }
}
