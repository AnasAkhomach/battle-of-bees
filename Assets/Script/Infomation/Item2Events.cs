using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item2Events : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag("TestInforItem2").GetComponent<TextInforType>().ShowText();
	}

	public void OnMouseDown( dfControl control, dfMouseEventArgs mouseEvent )
	{
		// Add event handler code here
		Debug.Log( "MouseDown" );
	}

}
