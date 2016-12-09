using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class DeleteSave : ButtonEventHandler {

	// Use this for initialization
	void Start ()
	{
		Init();
	}

	// Update is called once per frame
	void Update () {

	}

	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
		Save.DeleteAll();
		Application.Quit();
	}
}
