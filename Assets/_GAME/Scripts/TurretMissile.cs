using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMissile : TurretBase
{

	AudioSource _source;
	AudioSource source => _source ? _source : _source = GetComponent<AudioSource>();

	protected override void ActivateWeapon()
	{
		FindTarget(out target);
	}
	protected override void DeactivateWeapon()
	{
		//energyDelta = 0;
	}

	protected override void EnergyUse()
	{
		base.EnergyUse();
		if (!target)
			return;

		GameObject w = Instantiate(weapon, weapon.transform.position, weapon.transform.rotation);
		w.SetActive(true);
		ShotMissile missile = w.GetComponent<ShotMissile>();
		missile.Launch(target, this);
		source.Play();
	}

	protected override bool FindTarget(out EnemyBase enemy)
	{
		enemy = null;

		EnemyBase[] enemyList = FindObjectsOfType<EnemyBase>();
		if (enemyList.Length < 1)
			return false;

		float maxDistance = 0;
		foreach (EnemyBase e in enemyList)
		{
			float d = Vector3.Distance(transform.position, e.transform.position);
			if (d > range)
				continue;

			if (d < maxDistance)
				continue;

			maxDistance = d;
			enemy = e;
		}
		return true;
	}
}
