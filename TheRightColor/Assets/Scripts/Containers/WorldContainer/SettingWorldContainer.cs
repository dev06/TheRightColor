using UnityEngine;
using System.Collections;

public class SettingWorldContainer : MonoBehaviour {


	private Animation _animation;
	private bool _swipeDown;
	private bool _swipeUp;

	private bool _controlButtonPressed;
	private bool _triggeredEvent;
	void Start () {
		_animation = GetComponent<Animation>();
	}

	// Update is called once per frame
	void Update () {
		if (_triggeredEvent == false)
		{
			if (_swipeUp)
			{
				if (_animation.isPlaying == false)
				{
					if (EventManager.OnSwipeAninmationFinished != null)
					{
						EventManager.OnSwipeAninmationFinished();
					}
					_triggeredEvent = true;
				}
			} else {
				if (_swipeDown)
				{
					if (_animation.isPlaying == false)
					{
						if (EventManager.OnSwipeAninmationFinished != null)
						{
							EventManager.OnSwipeAninmationFinished();
						}
						_triggeredEvent = true;
					}
				}
			}
		}


	}
	void OnEnable()
	{
		EventManager.OnSwipeDown += OnSwipeDown;
		EventManager.OnSwipeUp += OnSwipeUp;
		EventManager.OnTimerUp += OnTimerUp;

	}
	void OnDisable()
	{
		EventManager.OnSwipeDown -= OnSwipeDown;
		EventManager.OnSwipeUp -= OnSwipeUp;
		EventManager.OnTimerUp -= OnTimerUp;

	}


	void OnSwipeUp()
	{



		if (GameManager.Instance.state == State.Setting)
		{
			_swipeUp = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "setting_world_down", 1);
		}


	}

	void OnSwipeDown()
	{

		if (GameManager.Instance.state == State.Menu)
		{
			_swipeDown = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "setting_world_down", -1);
		}



	}

	void OnTimerUp()
	{
		// if (GameManager.Instance.state == State.Game)
		// {
		// 	_swipeRight = true;
		// 	_triggeredEvent = false;
		// 	//StateManager.Instance.EnableCanvas("MenuCanvas");
		// 	AnimationController.Play(_animation, "menu_world_left", -1);
		// }
	}



}

