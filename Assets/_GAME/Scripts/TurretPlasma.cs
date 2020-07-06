using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlasma : TurretBase
{
	ShotPlasma _shot;
	ShotPlasma shot => _shot ? _shot : _shot = weapon.GetComponent<ShotPlasma>();
	AudioSource _source;
	AudioSource source => _source ? _source : _source = GetComponent<AudioSource>();


	protected override void OnScience(string scienceId)
	{
		base.OnScience(scienceId);
		shot.UpdateShot();
	}
	protected override void ActivateWeapon()
	{
		shot.SetBeam(target.TargetPoint.position);
		target.Damage(damage);
		Science.energy -= energyUse;
		energyDelta = 1;
		if (!source.isPlaying)
			source.Play();
	}
	protected override void DeactivateWeapon()
	{
		shot.SetBeam(shot.transform.position);
		source.Stop();
	}
}
