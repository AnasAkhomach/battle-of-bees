using UnityEngine;
using System.Collections;

public class SortingLayerScipt : MonoBehaviour {

    public string sortingLayerName;

    public void Start()
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < transforms.Length; i++)
        {
            GameObject gObject = transforms[i].gameObject;
            if (gObject.GetComponent<SpriteRenderer>() != null)
            {
                gObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
            }
        }
    }
}
