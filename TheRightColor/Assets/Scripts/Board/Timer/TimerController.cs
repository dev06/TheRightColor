using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerController : MonoBehaviour {

	public float baseTime;
	public float remainingTime;
	private float _timerVel;
	private GameObject _timerCounter;
	private Image _timerCounterImage;
	private bool _eventTriggered;
	private Animation _timerCounterAnimation;
	private bool _canDepleteTime;

	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
		EventManager.OnIncorrectColor += OnIncorrectColor;
		EventManager.OnBreakStreak += OnBreakStreak;
		EventManager.OnFirstTouch += OnFirstTouch;
		EventManager.OnRetryButtonPress += ResetBoard;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
		EventManager.OnIncorrectColor -= OnIncorrectColor;
		EventManager.OnFirstTouch -= OnFirstTouch;
		EventManager.OnBreakStreak -= OnBreakStreak;
		EventManager.OnRetryButtonPress -= ResetBoard;
	}


	void Start ()
	{
		_timerCounter = transform.FindChild("TimerCounter").gameObject;
		_timerCounterImage = _timerCounter.GetComponent<Image>();
		_timerCounterAnimation = _timerCounter.GetComponent<Animation>();

		baseTime = MasterVar.Base_Time;
		remainingTime = baseTime;
	}

	void Update ()
	{
		if (GameManager.Instance.state == State.Game)
		{

			if (GameManager.Instance.score > 0)
			{
				DepleteTime(1.0f);
			}

			if (!Application.isEditor)
			{
				if (_canDepleteTime)
				{

				}

			}



			if (_eventTriggered == false)
			{
				if (_timerCounterImage.fillAmount  <= 0)
				{
					if (EventManager.OnTimerUp != null)
					{
						EventManager.OnTimerUp();
					}
					_eventTriggered = true;
				}
			}

			TriggerTimerAlert(_timerCounterImage.fillAmount < MasterVar.ActivateTimerAlert);


		}
	}




	private void DepleteTime(float _penaltyRate)
	{

		if (remainingTime < 0)
		{
			remainingTime = 0;
		} else {
			remainingTime -= Time.deltaTime * _penaltyRate;
			_timerCounterImage.fillAmount  = Mathf.SmoothDamp(_timerCounterImage.fillAmount , remainingTime / baseTime, ref _timerVel, .1f);
		}
	}

	private void RepleteTime(float _additionalTime)
	{
		if (remainingTime < baseTime )
		{
			remainingTime += _additionalTime;
		}

		if (remainingTime > baseTime)
		{
			remainingTime = baseTime;
		}
	}

	private void OnFirstTouch()
	{
		_canDepleteTime = true;
	}

	private void OnBreakStreak()
	{
		remainingTime -= MasterVar.Incorrect_Color_Penalty / 4;
	}


	public void OnCorrectColor()
	{
		RepleteTime(MasterVar.Correct_Color_AdditionalTime);
	}

	public void OnIncorrectColor()
	{
		if (GameManager.Instance.score > 0)
		{
			remainingTime -= MasterVar.Incorrect_Color_Penalty;
		}
	}

	private void TriggerTimerAlert(bool state)
	{
		if (state)
		{
			_timerCounterAnimation.Play(_timerCounterAnimation.clip.name);
		} else {
			_timerCounterImage.color = new Color(1f, 1f, 1f, 1f);
			_timerCounterAnimation.Stop();
		}
	}

	private void ResetBoard()
	{
		// baseTime = MasterVar.Base_Time;
		// remainingTime = baseTime;
		// GameManager.Instance.score = 0;
		// GameManager.Instance.state = State.Menu;
		// _canDepleteTime = false;
		// _timerCounterImage.fillAmount = remainingTime;

		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}
}
