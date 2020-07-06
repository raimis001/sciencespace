using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BridgeButton : MonoBehaviour
{
	public UnityEvent OnClick;
	private void OnMouseDown()
	{
		//Debug.Log("Mouse click");
		OnClick?.Invoke();
	}
}
