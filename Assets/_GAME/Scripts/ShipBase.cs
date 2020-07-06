using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour
{
	public static ShipBase instance;

	public static Vector3 Position => instance.transform.position;

	private void Awake()
	{
		instance = this;
	}

	public void Damage(float damage)
	{
		Science.shipHull -= damage;
		//TODO explode ship
	}

}
