using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum StatusKind
{
	None, Build
}

public class Gui : MonoBehaviour
{
	static Gui instance;
	static StatusKind _Status = StatusKind.None;
	public static StatusKind Status
	{
		get
		{
			return _Status;
		}
		set
		{
			_Status = value;
			instance.buildIcon.gameObject.SetActive(Status == StatusKind.Build);
			instance.buildPlace.gameObject.SetActive(Status == StatusKind.Build);
		}
	}

	public GameObject[] turretsPrefabs;
	public LayerMask buildLayer;
	public LayerMask turretLayer;

	public Transform turretHolder;
	public Transform buildIcon;
	public BuildCursor buildPlace;

	public TMP_Text errortext;
	public float errorTime = 3;
	public AudioClip[] errorSounds;
	private float errorDelta;

	private AudioSource _source;
	private AudioSource source => _source ? _source : _source = GetComponent<AudioSource>();

	private int buildTurret;

	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		Status = StatusKind.None;
	}
	public void BuildTurret(int turret)
	{
		buildTurret = turret;
		Status = StatusKind.Build;
	}
	public static void ErrorMessage(string message, int sound)
	{
		instance.ShowError(message, sound);
	}

	void ShowError(string message, int sound)
	{
		errortext.text = message;
		source.clip = errorSounds[sound];
		source.Play();

		Color c = errortext.color;
		c.a = 1;
		errortext.color = c;
		errorDelta = errorTime;
	}

	private void Update()
	{
		if (errorDelta > 0)
		{
			errorDelta -= Time.deltaTime;
			Color c = errortext.color;
			c.a = errorDelta / errorTime;
			errortext.color = c;
		}

		if (Status != StatusKind.Build)
		{
			return;
		}
		if (Input.GetMouseButtonDown(1))
		{
			Status = StatusKind.None;
			return;
		}

		buildIcon.position = Input.mousePosition;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, buildLayer))
			return;

		bool canBuild = !Physics.CheckSphere(hit.point, 1, turretLayer);

		buildPlace.SetStatus(canBuild);
		buildPlace.transform.position = hit.point;

		if (!Input.GetMouseButtonDown(0))
			return;

		if (!canBuild)
		{
			//TODO errror message
			ShowError("Can't build here", 0);
			return;
		}

		string turretId = "";
		switch (buildTurret)
		{
			case 0:
				turretId = "laser";
				break;
			case 1:
				turretId = "plasma";
				break;
			case 2:
				turretId = "missile";
				break;
		}
		if (turretId == "")
			return;

		TurretData turret = Science.GetTurretById(turretId);
		if (turret == null)
			return;

		if (turret.price > Science.money)
		{
			//TODO mesage not money
			ShowError("Not enough money", 1);
			return;
		}
		Science.money -= turret.price;
		BuildTurret(hit.point);

		//TODO after build status going to none? yes
		Status = StatusKind.None;
	}

	void BuildTurret(Vector3 pos)
	{
		Instantiate(turretsPrefabs[buildTurret], pos, Quaternion.identity, turretHolder);
	}
}
