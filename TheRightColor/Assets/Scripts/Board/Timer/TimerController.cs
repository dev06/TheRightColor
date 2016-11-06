using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerController : MonoBehaviour {

	public float baseTime;
	public float remainingTime;
	private float _timerVel;
	private Image _timerCounterImage;
	private bool _eventTriggered;



	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
		EventManager.OnIncorrectColor += OnIncorrectColor;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
		EventManager.OnIncorrectColor -= OnIncorrectColor;

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



			if (!Application.isEditor) {
			}

			DepleteTime(1.0f);


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


	public void OnCorrectColor()
	{
		RepleteTime(MasterVar.Correct_Color_AdditionalTime);
	}

	public void OnIncorrectColor()
	{
		remainingTime -= MasterVar.Incorrect_Color_Penalty;
	}
}
