using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Tile :  MonoBehaviour, IPointerClickHandler {

	public TileID tileID;


	protected Image tileImage;
	protected Animation tileAnimation;

	public void Init()
	{
		tileImage = GetComponent<Image>();
		if (GetComponent<Animation>() != null)
		{
			tileAnimation = GetComponent<Animation>();
		}
	}

	public virtual void SetColor(Color c)
	{

	}

	public virtual void OnPointerClick(PointerEventData data)
	{

	}

}


public enum TileID
{
	Generator_Tile,
	Interactive_Tile,
}
