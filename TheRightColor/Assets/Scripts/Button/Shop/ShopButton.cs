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

		ELE_Particles,

		//AESTHETICS
		SUB_TRIANGLE,
		SUB_CIRCLE,
		SUB_SQUARE,
		SUB_BAR,
		SUB_BK_DEFAULT,

		//FEATURES
		SUB_MUSIC_FADE,
		SUB_PAUSE,
		SUB_SAVESCORE,

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
