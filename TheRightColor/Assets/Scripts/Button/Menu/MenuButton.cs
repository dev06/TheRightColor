using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MenuButton : ButtonEventHandler {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public override  void OnPointerClick(PointerEventData data)
	{
		switch (buttonID)
		{
			case ButtonID.Play:
			{
				if (EventManager.OnPlayButtonPress != null)
				{
					EventManager.OnPlayButtonPress();
				}
				break;
			}

			case ButtonID.Control:
			{
				if (EventManager.OnControlButtonPress != null) {
					EventManager.OnControlButtonPress();
				}
				break;
			}
		}
	}
}
