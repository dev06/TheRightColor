using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonEventHandler : MonoBehaviour, IPointerClickHandler {


	public ButtonID buttonID;
	void Start () {

	}

	void Update () {

	}


	public virtual void OnPointerClick(PointerEventData data)
	{

	}

}

public enum ButtonID
{
	Play,
	Setting,
	Credit,
	Control,
}
