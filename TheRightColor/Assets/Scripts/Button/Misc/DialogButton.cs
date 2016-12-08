using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DialogButton : ButtonEventHandler {

	void OnEnable()
	{

		EventManager.OnSubelementPress += OnSubelementPress;
	}

	void OnDisable()
	{

		EventManager.OnSubelementPress -= OnSubelementPress;
	}


	void Start ()
	{
		Init();
		GetComponent<Text>().raycastTarget = false;

	}

	void Update ()
	{

	}


	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
		switch (buttonID)
		{
			case ButtonID.DIA_Positive:
			{
				if (EventManager.OnDialogPositive != null)
				{
					EventManager.OnDialogPositive();
				}

				break;
			}
			case ButtonID.DIA_Negative:
			{
				if (EventManager.OnDialogNegative != null)
				{
					EventManager.OnDialogNegative();
				}

				break;
			}
		}

		GetComponent<Text>().raycastTarget = false;

	}

	void OnSubelementPress(ShopButton.Shop_ButtonID id)
	{
		GetComponent<Text>().raycastTarget = true;
	}

}
