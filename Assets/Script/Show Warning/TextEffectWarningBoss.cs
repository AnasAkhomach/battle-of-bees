using UnityEngine;
using System.Collections;

public class TextEffectWarningBoss : MonoBehaviour {

    float letterPause = 0.08f;
    string message = "";
    string str;

    public void ShowText()
    {
        message = gameObject.GetComponent<dfLabel>().Text;
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
