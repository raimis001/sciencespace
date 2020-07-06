using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class PulseLight : MonoBehaviour
{
	public float pulseVolume;

	[Range(0,10)]
	public float pulseSpeed;

	private Light _light;
	private Light pulseLight => _light ? _light : _light = GetComponent<Light>();
	private float startIntensity;

	private void Start()
	{
		startIntensity = pulseLight.intensity;
	}

	private void Update()
	{
		float p = Mathf.PingPong(Time.time * pulseSpeed, pulseVolume);
		pulseLight.intensity = startIntensity + p;
	}

}
