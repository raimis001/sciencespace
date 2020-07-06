using UnityEngine;
using System.Collections;
using System;
using UnityEngine.PlayerLoop;

public class EnemyBase : MonoBehaviour
{

	public float speed = 1;
	public Transform targetPoint;
	public Transform TargetPoint => targetPoint ? targetPoint : transform;

	public GameObject deadEffect;

	public float shotTime = 1;
	public float shotDistance = 50;
	public float minDistance = 8;
	public float damage = 0.02f;
	public GameObject shotEffect;

	internal float hitpoints = 1;
	private float shotDelay;

	private bool isDead;
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, ShipBase.Position, Time.deltaTime * speed);

		if (isDead)
			return;

		if (shotDelay > 0)
		{
			shotDelay -= Time.deltaTime;
			return;
		}
		float dist = Vector3.Distance(transform.position, ShipBase.Position);
		if (dist < minDistance)
			Die();

		if (dist > shotDistance)
			return;

		shotEffect.SetActive(true);
		shotEffect.transform.LookAt(ShipBase.instance.transform);
		shotDelay = shotTime;

	}

	internal void Damage(float damage)
	{
		if (isDead)
			return;

		hitpoints -= damage;
		if (hitpoints <= 0)
			Die();
	}

	private void Die()
	{
		Science.money += 10;
		isDead = true;
		Destroy(gameObject,0.1f);


		if (!deadEffect)
			return;

		GameObject obj = Instantiate(deadEffect, TargetPoint.position, Quaternion.identity);
		obj.SetActive(true);
		Destroy(obj, 5);
	}

}
