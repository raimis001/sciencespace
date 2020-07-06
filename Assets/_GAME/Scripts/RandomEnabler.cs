using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnabler : MonoBehaviour
{

	public float minTimer = 1;
	public float randTimer = 1;

	private readonly List<GameObject> childs = new List<GameObject>();

	private float timeDelta;
	private int lastElement = -1;
	void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			childs.Add(transform.GetChild(i).gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (timeDelta > 0)
		{
			timeDelta -= Time.deltaTime;
			return;
		}

		if (lastElement > -1)
			childs[lastElement].SetActive(false);

		lastElement = Random.Range(0, childs.Count);
		childs[lastElement].SetActive(true);

		timeDelta = minTimer + Random.value * randTimer;
	}
}
