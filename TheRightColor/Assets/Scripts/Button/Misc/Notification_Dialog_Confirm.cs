using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Notification_Dialog_Confirm : ButtonEventHandler {

	// Use this for initialization
	void Start ()
	{
		Init();
	}


	public override void OnPointerClick(PointerEventData data)
	{
		if (EventManager.OnDialogConfirm != null)
		{
			EventManager.OnDialogConfirm();
		}
	}
}
