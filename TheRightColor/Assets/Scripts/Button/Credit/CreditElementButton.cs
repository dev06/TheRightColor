using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class CreditElementButton : ButtonEventHandler {

	// Use this for initialization
	void Start () {
		Init();
	}

	// Update is called once per frame
	void Update () {

	}

	public override void OnPointerClick(PointerEventData data)
	{
		switch (buttonID)
		{
			case ButtonID.Alan_Walker_Fade:
				{
					Application.OpenURL(AppResources.Alan_Walker);
					break;
				}
		}
	}
}
