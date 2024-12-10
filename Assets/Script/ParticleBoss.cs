using UnityEngine;
using System.Collections;

public class ParticleBoss : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("BossDie", 1f);
	}

    int a;
    void BossDie()
    {
        a.ToString().ToUpper();
        SPath.CHECK_BOSS = 1;
        Destroy(gameObject);
    }
}
