using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerController : MonoBehaviour {

	public float baseTime;
	public float remainingTime;
	private float _timerVel;
	private Image _timerCounterImage;
	private bool _eventTriggered;

	private bool _canDepleteTime;

	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
		EventManager.OnIncorrectColor += OnIncorrectColor;
		EventManager.OnBreakStreak += OnBreakStreak;
		EventManager.OnFirstTouch += OnFirstTouch;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
		EventManager.OnIncorrectColor -= OnIncorrectColor;
		EventManager.OnFirstTouch -= OnFirstTouch;
		EventManager.OnBreakStreak -= OnBreakStreak;
	}


	void Start ()
	{
		_timerCounterImage = transform.FindChild("TimerCounter").GetComponent<Image>();
		baseTime = MasterVar.Base_Time;
		remainingTime = baseTime;
	}

	void Update ()
	{
		if (GameManager.Instance.state == State.Game)
		{


			if (_canDepleteTime)
			{
				DepleteTime(1.0f);
			}

			if (!Application.isEditor)
			{

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
		remainingTime -= MasterVar.Incorrect_Color_Penalty;
	}
}
