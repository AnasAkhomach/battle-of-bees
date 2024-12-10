using UnityEngine;
using System.Collections;

public class HitEffect : MonoBehaviour
{

    float stateTime;
    void Awake()
    {
        transform.localScale = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        stateTime += Time.deltaTime * 1.5f;
        transform.localScale = new Vector3(stateTime, stateTime, 1);
        if (stateTime > 0.7f)
            Destroy(gameObject);
    }
}
