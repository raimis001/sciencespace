using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShotPlasma : MonoBehaviour
{
  public float beamScale = 0.5f;

  public Transform rayImpact; // Impact transform
  public Transform rayMuzzle; // Muzzle flash transform

  LineRenderer lineRenderer;

  private TurretBase turret;

  void Awake()
  {
    lineRenderer = GetComponent<LineRenderer>();
    turret = GetComponentInParent<TurretBase>();
  }

  public void UpdateShot()
	{
    if (!turret)
      return;
	}

  public void SetBeam(Vector3 impact)
	{

    float beamLength = Vector3.Distance(transform.position, impact);

    lineRenderer.SetPosition(1, new Vector3(0, 0, beamLength));

    float propMult = beamLength * (beamScale / 10f);
    lineRenderer.material.SetTextureScale("_MainTex", new Vector2(propMult, 1f));

    if (rayImpact)
    {
      rayImpact.gameObject.SetActive(beamLength > 1);
      rayImpact.position = impact - transform.forward * 0.5f;
    }

    if (rayMuzzle)
    {
      rayMuzzle.gameObject.SetActive(beamLength > 1);
      rayMuzzle.position = transform.position + transform.forward * 0.1f;
    }
  }

}
