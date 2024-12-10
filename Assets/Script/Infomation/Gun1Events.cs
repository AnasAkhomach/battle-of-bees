using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun1Events : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag("TestInforGun1").GetComponent<TextInforType>().ShowText();
    }
	public void OnMouseDown( dfControl control, dfMouseEventArgs mouseEvent )
	{
		// Add event handler code here
		Debug.Log( "MouseDown" );
	}

}
