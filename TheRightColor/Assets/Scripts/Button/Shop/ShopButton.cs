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
		ELE_SPECTRUM,
		ELE_BACKGROUND,
		ELE_MUSIC,
		ELE_PAUSE,
		ELE_SAVE,
		ELE_EFFECTS,

		//AESTHETICS
		SUB_TRIANGLE,
		SUB_CIRCLE,
		SUB_SQUARE,
		SUB_BAR,
		SUB_BK_DEFAULT,
		SUB_BLOOM,
		SUB_FISHEYE,
		SUB_BK_SHADE,

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
