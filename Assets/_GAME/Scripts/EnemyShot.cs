using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
	public float damage = 0.1f;

	private EnemyBase _ship;
	private EnemyBase ship => _ship ? _ship : _ship = GetComponentInParent<EnemyBase>();

	private void OnParticleCollision(GameObject other)
	{
		ShipBase enemy = other.GetComponentInParent<ShipBase>();
		if (!enemy)
			return;

		enemy.Damage(ship ? ship.damage : damage);
	}
}
