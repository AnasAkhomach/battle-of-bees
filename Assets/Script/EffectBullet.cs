using UnityEngine;
using System.Collections;

public class EffectBullet : MonoBehaviour
{
    int rand;
    GameObject danObject;
    SpriteRenderer spriteRender;
    float alpha=1;
    // Use this for initialization
    void Start()
    {
        danObject = transform.Find("danroi").gameObject;
        spriteRender = danObject.GetComponent<SpriteRenderer>();
        rand = Random.Range(0, 2);
        if (rand == 0)
            GetComponent<Rigidbody2D>().AddForce(new Vector3(5, 200, 0));
        if (rand == 1)
            GetComponent<Rigidbody2D>().AddForce(new Vector3(-5, 200, 0));
    }

    void Update()
    {
        alpha -= Time.deltaTime*0.5f;
        spriteRender.color = new Color(1, 1, 1, alpha);
        if (transform.position.y < Camera.main.transform.position.y - 4)
            Destroy(gameObject);
    }
}
