using UnityEngine;
using System.Collections;

public class BossDemoScript : MonoBehaviour
{

    MyBezier bezier;
    float stateTime;
    public Transform effectHit;
    public Transform effectScore;
    // Use this for initialization
    void Start()
    {
        Vector2[] p = { new Vector2(5,0),new Vector2(4,0), new Vector2(3f,1),new Vector2(2f,2f),new Vector2(1,1f)
			,new Vector2(0f,0f),new Vector2(1f,-1f),new Vector2(2f,-2f),new Vector2(3f,-1f),new Vector2(4,0), new Vector2(5,0)};
        bezier = new MyBezier(p);
        Destroy(gameObject, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        stateTime += Time.deltaTime;
        bezier.update(stateTime / 6);
        transform.position = new Vector3(bezier.Position.x, bezier.Position.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals(SPath.TAG_BULLET))
            return;
        var bullet = other.gameObject as GameObject;
        if (bullet != null)
        {
            Instantiate(effectHit, new Vector3(transform.position.x, transform.position.y + 0.5f, -10), Quaternion.identity);
            MyPref.Save_Score_Current(2);
            SPath.Score_current += 2;
            var abc = Instantiate(effectScore, Camera.main.WorldToViewportPoint(new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), Random.Range(transform.position.y, transform.position.y + 2))), Quaternion.identity) as Transform;
            abc.GetComponent<ScoreEffect>().SetValue("+" + 2);
            Destroy(bullet);
        }
    }
}
