using UnityEngine;
using System.Collections;

public class MyRandom
{

    public bool nextBoolean()
    {
        int i = Random.Range(0, 2);
        return i == 1 ? true : false;
    }

    public float nextFloat(float min, float max)
    {
        return Random.Range(min, max);
    }
}
