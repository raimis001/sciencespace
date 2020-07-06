using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
	public string turretId;

	public Transform body;
	public GameObject weapon;

	[Range(1, 100)]
	public float range = 50;
	[Range(1,10)]
	public float minRange = 5;

	public float damage = 0.1f;
	public float fireRate = 2;
	public float weaponSpeed = 1;

	public float energyUse = 0.1f;
	private float energyTime = 1;
	protected float energyDelta;

	protected EnemyBase target;

	protected TurretData turretData;
	private void OnEnable()
	{
		Science.OnScienceComplete += OnScience;
	}
	private void OnDisable()
	{
		Science.OnScienceComplete -= OnScience;
	}

	protected virtual void OnScience(string scienceId)
	{
		if (turretData == null)
			return;

		foreach(var upgrade in turretData.level)
		{
			if (upgrade.scienceId != scienceId)
				continue;

			damage += upgrade.values.damage;
			energyUse -= upgrade.values.energy;
			fireRate += upgrade.values.fireRate;
			range += upgrade.values.range;
			weaponSpeed += upgrade.values.weaponSpeed;
		}
	}

	protected virtual void Start()
	{
		turretData = Science.GetTurretById(turretId);

		damage = turretData.values.damage;
		energyUse = turretData.values.energy;
		fireRate = turretData.values.fireRate;
		range = turretData.values.range;
		weaponSpeed = turretData.values.weaponSpeed;

		foreach (var upgrade in turretData.level)
		{
			TaskContainer task = Science.GetById(upgrade.scienceId);
			if (task == null || task.status != TaskKind.Ready)
				continue;

			damage += upgrade.values.damage;
			energyUse -= upgrade.values.energy;
			fireRate += upgrade.values.fireRate;
			range += upgrade.values.range;
			weaponSpeed += upgrade.values.weaponSpeed;
		}
	}

	protected virtual void ActivateWeapon()
	{
		if (!weapon)
			return;
		weapon.SetActive(true);
	}
	protected virtual void DeactivateWeapon()
	{
		if (!weapon)
			return;

		weapon.SetActive(false);
	}
	protected virtual void EnergyUse()
	{

	}

	private void Update()
	{
		if (!body)
			return;

		if (!target)
		{
			FindTarget(out target);
			if (!target)
			{
				DeactivateWeapon();
				return;
			}
		}

		if (Science.energy < energyUse)
		{
			DeactivateWeapon();
			return;
		}

		float distnce = Vector3.Distance(transform.position, target.transform.position);
		
		if (distnce > range || distnce < minRange)
		{
			target = null;
			return;
		}

		ActivateWeapon();

		body.LookAt(target.TargetPoint);

		if (energyDelta > 0)
		{
			energyDelta -= Time.deltaTime * fireRate;
			return;
		}

		energyDelta = energyTime;
		Science.energy -= energyUse;
		EnergyUse();
	}

	protected virtual bool FindTarget(out EnemyBase enemy)
	{
		enemy = null;

		EnemyBase[] enemyList = FindObjectsOfType<EnemyBase>();
		if (enemyList.Length < 1)
			return false;

		float maxDistance = Mathf.Infinity;
		foreach (EnemyBase e in enemyList)
		{
			float d = Vector3.Distance(transform.position, e.transform.position);
			if (d > range)
				continue;

			if (d > maxDistance)
				continue;

			maxDistance = d;
			enemy = e;
		}
		return true;
	}
}
