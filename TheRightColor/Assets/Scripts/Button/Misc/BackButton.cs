using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class BackButton : ButtonEventHandler {

	public State previousState;

	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);

		StateManager.Instance.SetState(previousState);


	}
}
