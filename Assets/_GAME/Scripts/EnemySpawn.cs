using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
  public EnemyBase[] enemyList;
  public float spawnTime = 3;
	public float randomTime = 2;
	public float maxEnemy = 10;

	float spawnTimer = 15;


	private void Update()
	{
		//TODO pofig par performanci - galvenais ātrums
		//EnemyBase[] currentEnemy = GetComponentsInChildren<EnemyBase>();
		//if (currentEnemy.Length >= maxEnemy)
		//	return;

		if (!CameraControll.GameStarted)
			return;

		if (spawnTimer > 0)
		{
			spawnTimer -= Time.deltaTime;
			return;
		}
		spawnTimer = spawnTime + Random.value * randomTime;
		EnemyBase enemy =  Instantiate(enemyList[Random.Range(0, enemyList.Length)], transform);
		//Todo enemy spawn procedure

	}
}
