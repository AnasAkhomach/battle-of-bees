using UnityEngine;
using System.Collections;

public class ItemMovement : MonoBehaviour
{

    bool checkPos;
    Vector3 speed = new Vector3(2, 0, 0);
    // Use this for initialization
    void Awake()
    {
        checkPos = transform.position.x <= Camera.main.transform.position.x ? true : false;
        speed = checkPos ? speed : -speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime);
        if ((checkPos && transform.position.x >= Camera.main.transform.position.x + 5) || (!checkPos && transform.position.x <= Camera.main.transform.position.x - 5))
            Destroy(gameObject);
    }
}
