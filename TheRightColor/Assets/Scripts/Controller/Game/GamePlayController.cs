using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GamePlayController : MonoBehaviour {

	public int consecutiveCount;

	private int stageOne;

	private Text _consectiveText;
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
		stageOne = 10;
		_consectiveText = GameObject.FindWithTag("Challenge/ConsecutveText").GetComponent<Text>();

	}

	void Update ()
	{

		_consectiveText.enabled = consecutiveCount > stageOne;

		if (consecutiveCount > stageOne)
		{
			MasterVar.Correct_Color_AdditionalTime = 1.0f;
		} else {
			MasterVar.Correct_Color_AdditionalTime = .8f;

		}
	}

	void OnCorrectColor()
	{
		consecutiveCount++;
		if (consecutiveCount > stageOne)
		{

			_consectiveText.text = "x" + consecutiveCount;
		}
	}


	void OnIncorrectColor()
	{

		if (consecutiveCount > stageOne)
		{
			if (EventManager.OnBreakStreak != null)
			{
				EventManager.OnBreakStreak();
			}
		}

		consecutiveCount = 0;
	}


}
