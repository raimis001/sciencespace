using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMissile : MonoBehaviour
{
	private EnemyBase target;
	private TurretBase turret;
	private bool detonated;
	public void Launch(EnemyBase enemy, TurretBase turret)
	{
		target = enemy;
		this.turret = turret;
	}

	private void Update()
	{
		if (!target)
		{
			Destroy(gameObject);
			return;
		}

		if (detonated)
			return;

		transform.LookAt(target.TargetPoint);

		transform.position = Vector3.MoveTowards(transform.position, target.TargetPoint.position, Time.deltaTime * turret.weaponSpeed);
		if (Vector3.Distance(transform.position, target.TargetPoint.position) < 3)
			Detonate();
	}

	private void Detonate()
	{
		detonated = true;
		target.Damage(1);
		Destroy(gameObject);
	}
}
