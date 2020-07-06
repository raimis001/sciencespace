using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum ViewKind
{
	Bridge, Turret, Science, Space, Begin
}

public class CameraControll : MonoBehaviour
{
	static CameraControll instance;
	static ViewKind _CurrentView = ViewKind.Begin;

	public static bool GameStarted => instance.introAnim.state != PlayState.Playing && !Science.isDead;
	public static ViewKind CurrentView
	{
		get { return _CurrentView; }
		set
		{
			ViewKind old = _CurrentView;
			_CurrentView = value;
			if (old == _CurrentView)
				return;

			PlayableDirector dir = instance.bridgeAnim;
			switch (_CurrentView)
			{
				case ViewKind.Science:
					dir = instance.scienceAnim;
					break;
				case ViewKind.Turret:
					dir = instance.turretAnim;
					break;
				case ViewKind.Bridge:
						dir = 
							old == ViewKind.Science ? instance.scienceBridgeAnim : 
							old == ViewKind.Begin ? instance.introAnim :
							instance.bridgeAnim;
					break;
				case ViewKind.Space:
					dir = instance.spaceAnim;
					break;
			}
			dir.Play();

			//Debug.LogFormat("Play {0}", dir.name);
			if (_CurrentView != ViewKind.Turret)
			{
				Gui.Status = StatusKind.None;
			}
		}
	}

	public PlayableDirector turretAnim;
	public PlayableDirector bridgeAnim;
	public PlayableDirector scienceAnim;
	public PlayableDirector scienceBridgeAnim;
	public PlayableDirector spaceAnim;
	public PlayableDirector introAnim;

	private void Awake()
	{
		instance = this;
	}
	private void Update()
	{

		if (_CurrentView == ViewKind.Begin || _CurrentView == ViewKind.Space)
			return;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (introAnim.state == PlayState.Playing)
			{
				if (introAnim.time < 33)
					introAnim.time = 33.3;
				return;
			}


			CurrentView = CurrentView == ViewKind.Science ? ViewKind.Turret : CurrentView == ViewKind.Bridge ? ViewKind.Science : ViewKind.Bridge;
		}
	}
	public static void ChangeView(ViewKind view)
	{
		CurrentView = view;
	}

	public void ChangeView(int view)
	{
		CurrentView = (ViewKind)view;
	}
	
	public void StartGame()
	{
		ChangeView(ViewKind.Bridge);
		Debug.Log("Start game");
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
