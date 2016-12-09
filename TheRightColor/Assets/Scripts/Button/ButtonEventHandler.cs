using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonEventHandler : MonoBehaviour, IPointerClickHandler {


	public ButtonID buttonID;

	protected Canvas _pausePanel;

	void Start ()
	{
		Init();
	}

	void Update () {

	}

	protected void Init()
	{
		_pausePanel = GameObject.FindWithTag("Canvas/PauseCanvas").GetComponent<Canvas>();
	}


	public virtual void OnPointerClick(PointerEventData data)
	{
		switch (buttonID)
		{
			case ButtonID.Pause:
			{
				GameManager.Instance.state = GameManager.Instance.state == State.Pause ? State.Game : State.Pause;
				// triggers an event for pause state or game state
				if (GameManager.Instance.state == State.Pause)
				{
					if (EventManager.OnPauseButtonPress != null)
					{
						EventManager.OnPauseButtonPress();
					}

					if (EventManager.OnPauseStateActive != null)
					{
						EventManager.OnPauseStateActive();
						_pausePanel.enabled = true;
					}


				} else if (GameManager.Instance.state == State.Game)
				{
					if (EventManager.OnGameStateActive != null)
					{
						EventManager.OnGameStateActive();
						_pausePanel.enabled = false;
					}
				}
				break;
			}
		}

	}

	IEnumerator ActivatePause()
	{
		yield return new WaitForSeconds(.15f);




	}

}

public enum ButtonID
{

	Play,
	Setting,
	Credit,
	Control,
	Alan_Walker_Fade,
	Pause,
	Resume,
	Retry,
	DIA_Positive,
	DIA_Negative,
	None,
}
