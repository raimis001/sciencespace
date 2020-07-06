using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : TurretBase
{
	protected override void OnScience(string scienceId)
	{
		base.OnScience(scienceId);
		UpdateShot();
	}

	protected override void Start()
	{
		base.Start();
		UpdateShot();
	}
	void UpdateShot()
	{
		Shot shot = GetComponentInChildren<Shot>();
		if (!shot)
			return;

		shot.UpdateShot();

	}
}