using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public enum TaskKind
{
	Imposible, Active, Working, Ready
}

[System.Serializable]
public class ScienceTask
{
	public string id;
	public string caption;
	public string description;
	public Sprite icon;
	public int price;
	public float time;
	public List<string> requested;
}

public class TaskContainer
{
	public ScienceTask task;

	internal bool working;
	internal bool completed = false;
	internal float progress = 0;

	public TaskKind status
	{
		get
		{
			if (completed)
				return TaskKind.Ready;

			if (working)
				return TaskKind.Working;

			if (CanResearch())
				return TaskKind.Active;

			return TaskKind.Imposible;
		}
	}

	private bool CanResearch()
	{
		foreach (string s in task.requested)
		{
			TaskContainer c = Science.GetById(s);
			if (c == null || !c.completed)
			  return false;
		}

		return true;
	}

}

public class Science : MonoBehaviour
{
	public static Science instance;

	public delegate void ScienceComplete(string scienceId);
	public static event ScienceComplete OnScienceComplete;

	public static float shipHull = 1f;

	public static int money = 10000;
	public static float energy = 50;
	public static float maxEnergy = 100;
	public GameObject dangerObject;

	public static bool isDead;

	public TMP_Text hullText;
	public TMP_Text[] moneyText;
	public TMP_Text energyText;

	public ScienceObject tasks;
	public TurretObject turrets;

	public readonly List<TaskContainer> taskList = new List<TaskContainer>();

	internal string currentTask = "";
	private float currentTime;

	public static TaskContainer GetById(string scienceId)
	{
		return instance.taskList.Find((t) => t.task.id == scienceId);
	}

	public static TurretData GetTurretById(string turretId)
	{
		return instance.turrets.turretList.Find((t) => t.id == turretId);
	}

	private void Awake()
	{
		instance = this;
		foreach (ScienceTask task in tasks.scienceList)
			taskList.Add(new TaskContainer() { task = task });
	}
	private void Start()
	{

	}

	public void StartTask(string taskId)
	{
		if (currentTask != "")
			return;

		TaskContainer task = GetById(taskId);
		if (task == null || task.status != TaskKind.Active)
			return;

		if (money < task.task.price)
		{
			Gui.ErrorMessage("Not enough money", 1);
			return;
		}

		money -= task.task.price;

		currentTask = taskId;

		task.working = true;
		currentTime = task.task.time;
	}

	private void Update()
	{
		if (isDead)
			return;

		if (shipHull <= 0)
		{
			isDead = true;
			CameraControll.ChangeView(ViewKind.Space);
			return;
		}

		dangerObject.SetActive(shipHull < 0.3f);//30%

		hullText.text = string.Format("{0:0}%", shipHull * 100f);
		foreach (TMP_Text t in moneyText)
			t.text = money.ToString();

		energyText.text = string.Format("{0:0}/{1:0}", energy, maxEnergy);

		if (currentTask == "")
			return;

		TaskContainer task = GetById(currentTask);
		if (!task.working)
		  return;

		if (currentTime > 0)
		{
			currentTime -= Time.deltaTime;
			task.progress = 1 - currentTime / task.task.time;
			return;
		}

		task.completed = true;
		task.working = false;
		task.progress = 1;

		OnScienceComplete?.Invoke(currentTask);

		currentTime = 0;
		currentTask = "";
	}
}
