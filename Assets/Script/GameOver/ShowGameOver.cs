using UnityEngine;
using System.Collections;

public class ShowGameOver : MonoBehaviour
{
    public void show()
    {
        if (!gameObject.GetComponent<dfTweenGroup>().IsPlaying)
            gameObject.GetComponent<dfTweenGroup>().Play();
    }

}
