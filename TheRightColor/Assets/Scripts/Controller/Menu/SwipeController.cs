using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class SwipeController : MonoBehaviour {

	private Vector2 _pointerDown;
	private Vector2 _pointerUp;
	private float _swipeDelay;
	private bool _canSwipe;
	void Start () {
		_canSwipe = true;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			_pointerDown = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_pointerUp = Input.mousePosition;
			if (_canSwipe)
			{
				CalculateSwipe();
			}
		}

		if (!_canSwipe)
		{
			_swipeDelay += Time.deltaTime;
		}

		if (_swipeDelay > .45f)
		{
			_canSwipe = true;
			_swipeDelay = 0;
		}


	}


	private void CalculateSwipe()
	{

		if (GameManager.Instance.state != State.Game)
		{
			float _abs_x = Mathf.Abs(_pointerUp.x - _pointerDown.x);
			float _abs_y = Mathf.Abs(_pointerUp.y - _pointerDown.y);

			if (_abs_x > _abs_y)
			{
				if (_abs_x > MasterVar.SwipeThreshold)
				{
					_canSwipe = false;
					float _difference = -(_pointerDown.x - _pointerUp.x);
					if (_difference > 0)
					{
						if (StateManager.Instance.stateArrayIndex_y == 1)
						{
							if (EventManager.OnSwipeLeft != null)
							{
								EventManager.OnSwipeLeft();
							}
						}

					} else if (_difference < 0)
					{

						if (StateManager.Instance.stateArrayIndex_y == 1)
						{
							if (EventManager.OnSwipeRight != null)
							{
								EventManager.OnSwipeRight();

							}
						}
					}
				}
			} else {
				if (_abs_y > MasterVar.SwipeThreshold)
				{
					_canSwipe = false;
					float _difference = -(_pointerDown.y - _pointerUp.y);
					if (_difference < 0)
					{
						if (StateManager.Instance.stateArrayIndex_x == 1)
						{
							if (EventManager.OnSwipeUp != null)
							{
								EventManager.OnSwipeUp();
							}
						}
					} else if (_difference > 0)
					{
						if (StateManager.Instance.stateArrayIndex_x == 1)
						{
							if (EventManager.OnSwipeDown != null)
							{
								EventManager.OnSwipeDown();
							}
						}

					}
				}
			}
		}
	}
}




