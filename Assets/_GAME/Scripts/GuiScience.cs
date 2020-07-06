using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuiScience : MonoBehaviour
{
	public string scienceID;
	public Image progressImage;
	public Image progressIcon;

	[Header("Price")]
	public GameObject pricePanel;
	public TMP_Text priceText;

	[Header("Active")]
	public GameObject activePanel;
	public Image iconImage;
	public GameObject requestedPanel;

	public Sprite[] stateIcons;

	private TaskContainer task;
	private TaskKind status;
	
	private Color iconColor;
	private Color workColor = Color.blue;
	private void Start()
	{
		
		task = Science.GetById(scienceID);
		if (task == null)
		{
			Debug.Log("science not defined " + scienceID, gameObject);
			return;
		}

		priceText.text = task.task.price.ToString();
		iconColor = iconImage.color;

		UpdateTask(scienceID);

		if (!requestedPanel)
			return;

		for (int i = 0; i < task.task.requested.Count; i++)
		{
			TaskContainer science = Science.GetById(task.task.requested[i]);

			Image child = requestedPanel.transform.GetChild(i).GetComponent<Image>();

			Color c = child.color;
			c.a = science.completed ? 1 : 0.2f;
			child.color = c;
		}

	}

	private void OnEnable()
	{
		Science.OnScienceComplete += ScienceChange;
	}

	private void OnDisable()
	{
		Science.OnScienceComplete -= ScienceChange;
	}
	void ScienceChange(string scienceID)
	{
		UpdateTask(scienceID);
	}

	private void Update()
	{
		if (task == null)
			return;

		progressImage.fillAmount = status == TaskKind.Working ? task.progress : 0;
		if (progressIcon)
			progressIcon.fillAmount = status == TaskKind.Working ? task.progress : 0;

		if (task.status != status)
			UpdateTask(scienceID);
	}

	private void UpdateTask(string scienceID)
	{

		if (this.scienceID == scienceID)
		{
			status = task.status;

			activePanel.SetActive(status == TaskKind.Active);

			if (stateIcons.Length < 4)
			{

				if (status == TaskKind.Working)
				{
					iconImage.color = workColor;
					return;
				}

				iconColor.a = status == TaskKind.Imposible ? 0.2f : 1;
				iconImage.color = iconColor;
				return;
			}

			if (progressIcon)
				progressIcon.sprite = stateIcons[2];

			iconImage.sprite =
				status == TaskKind.Imposible ? stateIcons[0] :
				status == TaskKind.Active ? stateIcons[1] :
				//status == TaskKind.Working ? stateIcons[2] :
				stateIcons[3];

			return;
		}
		if (!requestedPanel)
			return;

		int i = task.task.requested.IndexOf(scienceID);
		if (i < 0)
			return;

		Image child = requestedPanel.transform.GetChild(i).GetComponent<Image>();

		Color c = child.color;
		c.a = 1;
		child.color = c;

	}

	public void OnButton(BaseEventData eventData)
	{
		PointerEventData evt = (PointerEventData)eventData;
		if (evt.button == PointerEventData.InputButton.Left)
		{
			Science.instance.StartTask(scienceID);
		}

		if (evt.button == PointerEventData.InputButton.Right)
		{
			//TODO call desciption
			GuiScienceControll.ShowDescription(scienceID,((RectTransform)transform).position);
		}

	}
	public void MouseEnter()
	{
		pricePanel.SetActive(true);
	}

	public void MouseExit()
	{
		pricePanel.SetActive(false);
	}
}
