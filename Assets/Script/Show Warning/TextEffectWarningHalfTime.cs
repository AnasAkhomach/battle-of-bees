using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextEffectWarningHalfTime : MonoBehaviour {

    float letterPause = 0.08f;
    string message = "";
    public List<string> strArr;
    string str;
    // Use this for initialization
    //void Start()
    //{
    //    ShowText();
    //}

    public void ShowText()
    {
        message = strArr[Random.Range(0, strArr.Count)];
        gameObject.GetComponent<dfLabel>().Text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            gameObject.GetComponent<dfLabel>().Text += letter;
            //if (sound)
            //  audio.PlayOneShot(sound);
            //yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
        yield return new WaitForSeconds(1 + letterPause * (message.Length - 1));
        if (gameObject.GetComponent<dfLabel>().IsVisible)
            gameObject.GetComponent<dfLabel>().IsVisible = false;
    }
}
