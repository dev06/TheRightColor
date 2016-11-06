using UnityEngine;
using System.Collections;

public class GameWorldContainer : MonoBehaviour {


	private Animation _animation;
	private bool _swipeRight;
	private bool _swipeLeft;
	private bool _timerUp;
	private bool _controlButtonPressed;
	private bool _triggeredEvent;
	void Start () {
		_animation = GetComponent<Animation>();
	}

	// Update is called once per frame
	void Update () {
		if (_triggeredEvent == false)
		{

			if (_timerUp)
			{
				if (_animation.isPlaying == false)
				{
					StartCoroutine("Reload");
				}
			}
		}
	}

	IEnumerator Reload()
	{
		yield return new WaitForSeconds(.5f);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
		_timerUp = false;
	}
	void OnEnable()
	{
		EventManager.OnSwipeRight += OnSwipeRight;
		EventManager.OnTimerUp += OnTimerUp;

	}
	void OnDisable()
	{
		EventManager.OnSwipeRight -= OnSwipeRight;
		EventManager.OnTimerUp -= OnTimerUp;

	}



	void OnSwipeRight()
	{

		if (GameManager.Instance.state == State.Menu)
		{
			_swipeRight = true;
			_triggeredEvent = false;
			AnimationController.Play(_animation, "game_world_right", -1);
		}

	}


	void OnTimerUp()
	{
		if (StateManager.Instance.stateArrayIndex_y == 1)
		{
			if (GameManager.Instance.state == State.Game)
			{
				_timerUp = true;
				_swipeRight = true;
				_triggeredEvent = false;
				AnimationController.Play(_animation, "game_world_right", 1);
			}
		}
	}
}


