using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBase : MonoBehaviour
{

	public float energyFill = 0.5f;
	public float energyTime = 0.5f;

	private float energyDelta;

	private void Update()
	{
		if (energyDelta > 0)
		{
			energyDelta -= Time.deltaTime;
			return;
		}

		energyDelta = energyTime;
		Science.energy = Mathf.Clamp(Science.energy + energyFill, 0, Science.maxEnergy);
	}

}
