using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ShopButton : MonoBehaviour, IPointerClickHandler {

	public delegate void ShopButtonClick(Shop_ButtonID id);
	public static ShopButtonClick OnShopButtonClick;

	public enum Shop_ButtonID
	{
		None,
		Aesthetics,
		Features,
		Achievements,
		Particles,
	}

	public Shop_ButtonID buttonID;

	void Start ()
	{

	}

	void Update ()
	{

	}

	public virtual void OnPointerClick(PointerEventData data)
	{
		if (OnShopButtonClick != null)
		{
			OnShopButtonClick(buttonID);
		}
	}
}
