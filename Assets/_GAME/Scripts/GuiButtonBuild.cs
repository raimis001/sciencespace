using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuiButtonBuild : MonoBehaviour
{
  public string turretId;

  public GameObject pricePanel;
  public TMP_Text priceText;

	private TurretData turret;

	private void Start()
	{
		if (!pricePanel)
			return;

		pricePanel.SetActive(false);
		turret = Science.GetTurretById(turretId);
		if (turret == null)
			return;
		priceText.text = turret.price.ToString();
	}

	public void MouseEnter()
	{
		if (turret == null)
			return;
		pricePanel.SetActive(true);
	}

	public void MouseExit()
	{
		if (turret == null)
			return;
		pricePanel.SetActive(false);
	}

}
