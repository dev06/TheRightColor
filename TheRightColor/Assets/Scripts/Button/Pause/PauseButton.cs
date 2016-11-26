using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseButton : ButtonEventHandler {

	void Start ()
	{
		Init();
	}

	void Update () {

	}

	public override void OnPointerClick(PointerEventData data)
	{
		switch (buttonID)
		{
			case ButtonID.Resume:
				{
					if (EventManager.OnGameStateActive != null)
					{
						EventManager.OnGameStateActive();
						GameManager.Instance.state = State.Game;
					}
					break;
				}
			case ButtonID.Retry:
				{
					if (EventManager.OnRetryButtonPress != null)
					{
						EventManager.OnRetryButtonPress();
						GameManager.Instance.state = State.Game;
					}
					break;
				}
		}


		_pausePanel.enabled = false;

	}
// 	public override void OnPointerClick(PointerEventData data)
// 	{

// 		// switch (buttonID)
// 		// {
// 		// 	case ButtonID.Resume:
// 		// 		{
	// 		}

// 		// 	case ButtonID.Retry:
// 		// 		{
// 		// 			if (EventManager.OnRetryButtonPress != null)
// 		// 			{
// 		// 				EventManager.OnRetryButtonPress();
// 		// 			}
// 		// 			break;
// 		// 		}
// 		// }

// //		_pausePanel.enabled = false;

// 	}
}
