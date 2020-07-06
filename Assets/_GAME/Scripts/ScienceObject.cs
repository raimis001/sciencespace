using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScienceData", menuName = "Scriptable/ScienceData")]
public class ScienceObject : ScriptableObject
{
	public List<ScienceTask> scienceList;
}

