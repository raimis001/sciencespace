using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiScienceControll : MonoBehaviour
{
	public static GuiScienceControll instance;

	public GuiDescription descriptionPanel;

	public static void ShowDescription(string scienceID, Vector3 pos)
	{

		Plane plane = new Plane(instance.descriptionPanel.transform.forward, instance.descriptionPanel.transform.position);
		// create a ray from the mousePosition
		Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(pos) + new Vector3(40,10,0));
		// plane.Raycast returns the distance from the ray start to the hit point
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			// some point of the plane was hit - get its coordinates
			Vector3 hitPoint = ray.GetPoint(distance);
			instance.descriptionPanel.transform.position = hitPoint;

			TaskContainer task = Science.GetById(scienceID);

			instance.descriptionPanel.caption = task.task.caption;
			instance.descriptionPanel.description = task.task.description;
			instance.descriptionPanel.iconImage.sprite = task.task.icon;

			instance.descriptionPanel.gameObject.SetActive(true);
		}


		
	}
	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		descriptionPanel.gameObject.SetActive(false);
	}
}
