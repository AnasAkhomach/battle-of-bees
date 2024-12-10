using UnityEngine;
using System.Collections;

public class SwitchGun : MonoBehaviour
{

    public enum Type
    {
        NORMAL, FIRE
    }

    public Type type { get; set; }

    void Awake()
    {
        type = Type.NORMAL;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
