using UnityEngine;
using System.Collections;

public class TextInforType : MonoBehaviour
{

    float letterPause = 0.01f;
    string message = "";
    string str;
    // Use this for initialization
    void Start()
    {
        b = false;
        message = gameObject.GetComponent<dfLabel>().Text;
        gameObject.GetComponent<dfLabel>().Text = "";
        StartCoroutine(TypeText());
        //InvokeRepeating("ShowText", Random.Range(5, 8), Random.Range(5, 8));
    }

    public void ShowText()
    {
      //  StopText();
        b = false;
        message = gameObject.GetComponent<dfLabel>().Text;
        gameObject.GetComponent<dfLabel>().Text = "";
        StartCoroutine(TypeText());
    }

    public void StopText()
    {
        b = true;
        gameObject.GetComponent<dfLabel>().Text = str;
//        gameObject.GetComponent<dfLabel>().Text = "Day la ong bom aaaaaaaaa" + "\n" +
//"Day la ong bom aaaaaaaaa" + "\n" +
//"Day la ong bom aaaaaaaaa" + "\n" +
//"Day la ong bom aaaaaaaaa" + "\n" +
//"Day la ong bom aaaaaaaaa";
    }


    bool b;
    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            if (b)
                break;
            gameObject.GetComponent<dfLabel>().Text += letter;
            //if (sound)
            //  audio.PlayOneShot(sound);
            //yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
}
