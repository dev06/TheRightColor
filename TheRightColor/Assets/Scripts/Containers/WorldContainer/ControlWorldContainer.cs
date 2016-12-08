using UnityEngine;
using System.Collections;

public class ControlWorldContainer : MonoBehaviour {


	private Animation _animation;
	private bool _swipeRight;
	private bool _swipeLeft;

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
					// if (EventManager.OnSwipeAninmationFinished != null)
					// {
					// 	EventManager.OnSwipeAninmationFinished();
					// }
					_triggeredEvent = true;

				}
			} else {
				if (_swipeRight)
				{
					if (_animation.isPlaying == false)
					{
						// if (EventManager.OnSwipeAninmationFinished != null)
						// {
						// 	EventManager.OnSwipeAninmationFinished();
						// }
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

	}
	void OnDisable()
	{
		EventManager.OnSwipeRight -= OnSwipeRight;
		EventManager.OnSwipeLeft -= OnSwipeLeft;

	}



	void OnSwipeRight()
	{
		if (StateManager.Instance.stateArrayIndex_y == 1)
		{
			if (GameManager.Instance.state == State.Control)
			{
				_swipeRight = true;
				_triggeredEvent = false;
				AnimationController.Play(_animation, "control_world_left", 1);

			}
		}

	}

	void OnSwipeLeft()
	{
		if (StateManager.Instance.stateArrayIndex_y == 1)
		{
			if (GameManager.Instance.state == State.Menu)
			{
				_swipeLeft = true;
				_triggeredEvent = false;
				AnimationController.Play(_animation, "control_world_left", -1);
			}
		}
	}



}

