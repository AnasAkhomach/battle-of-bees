using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OKGun3Events : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		GameObject.FindGameObjectWithTag("TestInforGun3").GetComponent<TextInforType>().StopText();
	}

	public void OnMouseDown( dfControl control, dfMouseEventArgs mouseEvent )
	{
		// Add event handler code here
		Debug.Log( "MouseDown" );
	}

}
