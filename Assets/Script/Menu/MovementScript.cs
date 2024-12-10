using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{

    MyBezier myBezier;


    void Start()
    {
        Vector2[] p = { new Vector2(-5, Random.Range(1, 3)), new Vector2(-1f, Random.Range(-2, 4)), new Vector2(-2f, Random.Range(-2, 4)), new Vector2(-2.5f, Random.Range(-2, 4)), new Vector2(0f, Random.Range(-2, 4)), new Vector2(1f, Random.Range(-2, 4)), new Vector2(2f, Random.Range(-2, 4)), new Vector2(2.5f, Random.Range(-2, 3)), new Vector2(5f, Random.Range(-2, 3)) };
        Vector2[] p2 = { new Vector2(5, Random.Range(1, 3)), new Vector2(1f, Random.Range(-2, 4)), new Vector2(2f, Random.Range(-2, 4)), new Vector2(2.5f, Random.Range(-2, 4)), new Vector2(0f, Random.Range(-2, 4)), new Vector2(-1f, Random.Range(-2, 4)), new Vector2(-2f, Random.Range(-2, 4)), new Vector2(-2.5f, Random.Range(-2, 3)), new Vector2(-5f, Random.Range(-2, 3)) };
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            myBezier = new MyBezier(p);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            myBezier = new MyBezier(p2);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Destroy(gameObject, 10);
    }

    float stateTime;
    // Update is called once per frame
    void Update()
    {
        stateTime += Time.deltaTime;
        myBezier.update(stateTime / 10);
        transform.position = new Vector3(myBezier.Position.x, myBezier.Position.y, -2);
    }
}
