using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
	public float damage = 0.1f;

	public SoundRandom startSound;

	TurretBase _turret;
	TurretBase turret => _turret ? _turret : _turret = GetComponentInParent<TurretBase>();
	ParticleSystem ps;
	int particles;

	private void Awake()
	{
		ps = GetComponent<ParticleSystem>();
	}

	private void Start()
	{
		UpdateShot();
	}
	public void UpdateShot()
	{
		ps.Stop();
		var em = ps.emission;
		em.rateOverTime = turret ? turret.fireRate : 1;
		ps.Play();
	}
	private void OnParticleTrigger()
	{
		Debug.Log("Patrticle trigger");
	}

	private void OnParticleCollision(GameObject other)
	{
		EnemyBase enemy = other.GetComponentInParent<EnemyBase>();
		if (!enemy)
			return;

		enemy.Damage(turret ? turret.damage : damage);
	}
	void Update()
	{

		if (ps.particleCount < particles)
		{
			//Debug.Log("Particle die");
		}

		if (ps.particleCount > particles)
		{
			//Debug.Log("Spawn particle");
			startSound.Play();
		}

		particles = ps.particleCount;
	}
}
