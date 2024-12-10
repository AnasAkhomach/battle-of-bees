using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btFaceBookEvents : MonoBehaviour
{

    public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
    {
        GameObject.FindGameObjectWithTag(SPath.TAG_AUDIO).GetComponent<AudioController>().playSound(0);
        FacebookShare.postLinkToFacebook("Let’s fight bravely and self-discovery the features of Battle of the bees");
    }
}
