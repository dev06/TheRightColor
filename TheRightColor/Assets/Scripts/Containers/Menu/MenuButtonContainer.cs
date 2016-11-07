using UnityEngine;
using System.Collections;

public class MenuButtonContainer : MonoBehaviour {

	private Animation _animation;
	private bool _swipeRight;
	private bool _swipeLeft;

	private bool _controlButtonPressed;
	private bool _triggeredEvent;
	void Start ()
	{
		_animation = GetComponent<Animation>();
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
	// Update is called once per frame
	void Update ()
	{

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

		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_swipeLeft = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_swipe_left_anim", 1);
		} else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			_swipeRight = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_swipe_right_anim", 1);
		}
	}

	void OnSwipeRight()
	{

		if (GameManager.Instance.state == State.Control)
		{
			_swipeRight = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_swipe_right_anim", 1);
			Debug.Log("control -> Menu");
		}

		if (GameManager.Instance.state == State.Menu)
		{
			_swipeRight = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_swipe_left_anim", 1);
			Debug.Log(" Menu -> Game");

		}



	}

	void OnSwipeLeft()
	{
		if (GameManager.Instance.state == State.Game)
		{
			_swipeLeft = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_swipe_left_anim", 1);
		}

		if (GameManager.Instance.state == State.Menu)
		{
			_swipeLeft = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "menu_swipe_right_anim", 1);
		}

	}


	public void OnButtonTouch()
	{
		if (EventManager.OnButtonTouch != null)
		{
			EventManager.OnButtonTouch();
		}
	}
}
