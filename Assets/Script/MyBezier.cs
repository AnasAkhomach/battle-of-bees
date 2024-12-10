using UnityEngine;
using System.Collections;

public sealed class MyBezier
{

    private float x, y;
    private int n;
    private Vector2[] p;
    private float[] param;
    private Vector2 cur = new Vector2();
    private Vector2 old = new Vector2();

    
    public MyBezier(Vector2[] p)
    {
        this.p = p;
        this.n = p.Length - 1;
        param = getParamBenzier(n);
    }

    public Vector2 Position
    {
        set { x = value.x; y = value.y; }
        get { return new Vector2(x, y); }
    }

    public float[] getParamBenzier(int n)
    {
        float[] val = new float[n + 1];
        for (int i = 0; i <= n; i++)
        {
            val[i] = binomial(n, i);
        }
        return val;
    }

    public void update(float t)
    {
        old.x = x;
        old.y = y;

        float px = 0, py = 0;
        for (int i = 0; i <= n; i++)
        {
            float a = param[i];
            px += a * (float)(Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * p[i].x);
            py += a * (float)(Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * p[i].y);
        }
        x = px;
        y = py;

        cur.x = x;
        cur.y = y;
        cur.x -= old.x;
        cur.y -= old.y;
    }


    public float getAngle()
    {
        float angle = (float)(Mathf.Atan2(cur.y, cur.x) * (180 / Mathf.PI));
        if (angle < 0) angle += 360;
        return Mathf.Deg2Rad * (angle - 90);
    }

    //giai thừa
    private float factorial(int n)
    {
        if (n <= 1) return 1;
        return factorial(n - 1) * n;
    }

    private float binomial(int n, int i)
    {
        float _n = factorial(n);
        float _i = factorial(i);
        float n_i = factorial(n - i);
        return _n / (_i * n_i);
    }
}
