using UnityEngine;
using System.Collections;

public class MenuWorldContainer : MonoBehaviour {


	private Animation _animation;
	private bool _swipeRight;
	private bool _swipeLeft;
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
			if (_swipeLeft)
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
				if (_swipeRight)
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
		EventManager.OnSwipeRight += OnSwipeRight;
		EventManager.OnSwipeLeft += OnSwipeLeft;
		EventManager.OnSwipeUp += OnSwipeUp;
		EventManager.OnSwipeDown += OnSwipeDown;

		EventManager.OnTimerUp += OnTimerUp;
		EventManager.OnTimerUp += OnTimerUp;

	}
	void OnDisable()
	{
		EventManager.OnSwipeRight -= OnSwipeRight;
		EventManager.OnSwipeLeft -= OnSwipeLeft;
		EventManager.OnSwipeDown -= OnSwipeDown;
		EventManager.OnSwipeUp -= OnSwipeUp;
		EventManager.OnTimerUp -= OnTimerUp;

	}



	void OnSwipeRight()
	{

		if (GameManager.Instance.state == State.Control)
		{
			_swipeRight = true;
			_triggeredEvent = false;
			StateManager.Instance.EnableCanvas("MenuCanvas");
			AnimationController.Play(_animation, "menu_world_right", -1);
		}

		if (GameManager.Instance.state == State.Menu)
		{
			_swipeRight = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_world_left", 1);
		}



	}

	void OnSwipeLeft()
	{

		if (GameManager.Instance.state == State.Game)
		{
			_swipeRight = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_world_left", -1);
		}

		if (GameManager.Instance.state == State.Menu)
		{
			_swipeRight = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_world_right", 1);
		}



	}



	void OnSwipeUp()
	{
		if (GameManager.Instance.state == State.Setting)
		{
			_swipeUp = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_world_up", -1);
		}



		if (GameManager.Instance.state == State.Menu)
		{
			_swipeUp = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_world_down", 1);
		}



	}

	void OnSwipeDown()
	{
		if (GameManager.Instance.state == State.Menu)
		{
			_swipeDown = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_world_up", 1);
		}



		if (GameManager.Instance.state == State.Credit)
		{
			_swipeDown = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_world_down", -1);
		}


	}

	void OnTimerUp()
	{
		if (_animation != null)
		{
			if (GameManager.Instance.state == State.Game)
			{
				_swipeRight = true;
				_triggeredEvent = false;
				//StateManager.Instance.EnableCanvas("MenuCanvas");
				AnimationController.Play(_animation, "menu_world_left", -1);
			}
		}

	}



}
