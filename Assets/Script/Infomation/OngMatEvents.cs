using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OngMatEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GameObject.FindGameObjectWithTag("TestInforOngMat").GetComponent<TextInforType>().ShowText();
	}

	public void OnMouseDown( dfControl control, dfMouseEventArgs mouseEvent )
	{
		// Add event handler code here
		Debug.Log( "MouseDown" );
	}

}
