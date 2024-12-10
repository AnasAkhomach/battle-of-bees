using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    GameObject gun1, gun2, gun3, gun4;
    GameObject normal1, normal2, normal3, normal4;
    GameObject fire1, fire2, fire3, fire4;
    Animator anim1, anim2, anim3, anim4;
    SwitchGun switchGun;

    void Awake()
    {
        gun1 = transform.Find("Gun1").gameObject;
        gun2 = transform.Find("Gun2").gameObject;
        gun3 = transform.Find("Gun3").gameObject;
        gun4 = transform.Find("Gun4").gameObject;

        normal1 = gun1.transform.Find("normal").gameObject;
        normal2 = gun2.transform.Find("normal").gameObject;
        normal3 = gun3.transform.Find("normal").gameObject;
        normal4 = gun4.transform.Find("normal").gameObject;

        fire1 = gun1.transform.Find("fire").gameObject;
        fire2 = gun2.transform.Find("fire").gameObject;
        fire3 = gun3.transform.Find("fire").gameObject;
        fire4 = gun4.transform.Find("fire").gameObject;

        anim1 = fire1.GetComponent<Animator>();
        anim2 = fire2.GetComponent<Animator>();
        anim3 = fire3.GetComponent<Animator>();
        anim4 = fire4.GetComponent<Animator>();

        switchGun = transform.GetComponent<SwitchGun>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag(SPath.TAG_GAME_SCREEN).GetComponent<GameScreen>().State == GameScreen.GState.GAME_WIN ||
           GameObject.FindGameObjectWithTag(SPath.TAG_GAME_SCREEN).GetComponent<GameScreen>().State == GameScreen.GState.GAME_LOSE)
            return;
        switch (MyPref.getGun())
        {
            case 1:
                setGun1();

                switch (switchGun.type)
                {
                    case SwitchGun.Type.NORMAL:
                        normal1.SetActive(true);
                        fire1.SetActive(false);
                        break;
                    case SwitchGun.Type.FIRE:
                        normal1.SetActive(false);
                        fire1.SetActive(true);
                        if (anim1.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
                            switchGun.type = SwitchGun.Type.NORMAL;
                        break;
                }


                break;
            case 2:
                setGun2();

                switch (switchGun.type)
                {
                    case SwitchGun.Type.NORMAL:
                        normal2.SetActive(true);
                        fire2.SetActive(false);
                        break;
                    case SwitchGun.Type.FIRE:
                        normal2.SetActive(false);
                        fire2.SetActive(true);
                        if (anim2.GetCurrentAnimatorStateInfo(0).normalizedTime >= 01f)
                            switchGun.type = SwitchGun.Type.NORMAL;
                        break;
                }
                break;
            case 3:
                setGun3();
                switch (switchGun.type)
                {
                    case SwitchGun.Type.NORMAL:
                        normal3.SetActive(true);
                        fire3.SetActive(false);
                        break;
                    case SwitchGun.Type.FIRE:
                        normal3.SetActive(false);
                        fire3.SetActive(true);
                        if (anim3.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                            switchGun.type = SwitchGun.Type.NORMAL;
                        break;
                }
                break;
            case 4:
                setGun4();
                switch (switchGun.type)
                {
                    case SwitchGun.Type.NORMAL:
                        normal4.SetActive(true);
                        fire4.SetActive(false);
                        break;
                    case SwitchGun.Type.FIRE:
                        normal4.SetActive(false);
                        fire4.SetActive(true);
                        if (anim4.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                            switchGun.type = SwitchGun.Type.NORMAL;
                        break;
                }
                break;
            default:
                Debug.LogError("error: GunController" + gameObject);
                return;
        }
    }

    void switch_Gun()
    {

    }

    public void setGun1()
    {
        gun1.SetActive(true);
        gun2.SetActive(false);
        gun3.SetActive(false);
        gun4.SetActive(false);
    }

    public void setGun2()
    {
        gun1.SetActive(false);
        gun2.SetActive(true);
        gun3.SetActive(false);
        gun4.SetActive(false);
    }
    public void setGun3()
    {
        gun1.SetActive(false);
        gun2.SetActive(false);
        gun3.SetActive(true);
        gun4.SetActive(false);
    }
    public void setGun4()
    {
        gun1.SetActive(false);
        gun2.SetActive(false);
        gun3.SetActive(false);
        gun4.SetActive(true);
    }
}
