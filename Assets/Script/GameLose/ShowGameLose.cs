using UnityEngine;
using System.Collections;

public class ShowGameLose : MonoBehaviour
{
    public void showLose()
    {
        if (!gameObject.GetComponent<dfTweenGroup>().IsPlaying)
            gameObject.GetComponent<dfTweenGroup>().Play();
    }
}
