using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{

	public float startSpeed = 1;
	public float randomSpeed = 1;

	private float speed;
	private void Start()
	{
		speed = startSpeed + (Random.value - 0.5f) * randomSpeed;
	}

	void Update()
	{
		transform.Rotate(0, 0, Time.deltaTime * speed);
	}
}
