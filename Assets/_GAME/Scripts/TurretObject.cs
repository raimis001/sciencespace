using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TurretProps
{
	public float damage;
	public float energy;
	public float fireRate;
	public float range;
	public float weaponSpeed;
}

[System.Serializable]
public class TurretUpgrade
{
	public string scienceId;
	public TurretProps values;
}

[System.Serializable]
public class TurretData
{
	public string id;
	public int price;

	public TurretProps values;

	public TurretUpgrade[] level;
}

[CreateAssetMenu(fileName = "TurretData", menuName = "Scriptable/TurretData")]
public class TurretObject : ScriptableObject
{
	public List<TurretData> turretList;
}