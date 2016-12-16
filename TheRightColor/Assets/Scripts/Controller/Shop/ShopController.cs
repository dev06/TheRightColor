using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
public class ShopController : MonoBehaviour {

	public static ShopController Instance;

	private GameObject OBJ_CAMERA;
	private GameObject OBJ_EFFECTS;
	private GameObject OBJ_TRIANGLE;
	private GameObject OBJ_SQUARE;
	private GameObject OBJ_CIRCLE;
	private GameObject OBJ_SPECTRUM;

	private GameObject OBJ_BK_SHADE;
	private GameObject OBJ_BK_DEFAULT;

	void Awake()
	{
		Instance = this;
	}

	void Start ()
	{
		OBJ_EFFECTS = GameObject.FindWithTag("Effects").gameObject;
		OBJ_TRIANGLE = OBJ_EFFECTS.transform.FindChild("Triangle").gameObject;
		OBJ_SQUARE = OBJ_EFFECTS.transform.FindChild("Square").gameObject;
		OBJ_CIRCLE = OBJ_EFFECTS.transform.FindChild("Circle").gameObject;
		OBJ_SPECTRUM = OBJ_EFFECTS.transform.FindChild("AudioSpecturm").FindChild("BarContainer").gameObject;
		OBJ_BK_DEFAULT = OBJ_EFFECTS.transform.FindChild("BK_DEFAULT").gameObject;
		OBJ_BK_SHADE = OBJ_EFFECTS.transform.FindChild("BK_ONE").gameObject;
		OBJ_CAMERA = Camera.main.gameObject;

		ActivateOnStart(ShopButton.Shop_ButtonID.SUB_TRIANGLE);
		ActivateOnStart(ShopButton.Shop_ButtonID.SUB_SQUARE);
		ActivateOnStart(ShopButton.Shop_ButtonID.SUB_BLOOM);
		ActivateOnStart(ShopButton.Shop_ButtonID.SUB_FISHEYE);
		ActivateOnStart(ShopButton.Shop_ButtonID.SUB_CIRCLE);
		ActivateOnStart(ShopButton.Shop_ButtonID.SUB_BAR);
		ActivateOnStart(ShopButton.Shop_ButtonID.SUB_BK_DEFAULT);
		ActivateOnStart(ShopButton.Shop_ButtonID.SUB_BK_SHADE);
	}

	void ActivateOnStart(ShopButton.Shop_ButtonID id)
	{
		switch (id)
		{
			case ShopButton.Shop_ButtonID.SUB_TRIANGLE: 	OBJ_TRIANGLE.SetActive(Save.GetBool(id + "Active")); 								break;
			case ShopButton.Shop_ButtonID.SUB_SQUARE: 		OBJ_SQUARE.SetActive(Save.GetBool(id + "Active"));									break;
			case ShopButton.Shop_ButtonID.SUB_BLOOM: 		OBJ_CAMERA.GetComponent<BloomOptimized>().enabled = Save.GetBool(id + "Active"); 	break;
			case ShopButton.Shop_ButtonID.SUB_FISHEYE: 		OBJ_CAMERA.GetComponent<Fisheye>().enabled = Save.GetBool(id + "Active"); 			break;
			case ShopButton.Shop_ButtonID.SUB_CIRCLE:		OBJ_CIRCLE.SetActive(Save.GetBool(id + "Active")); 									break;
			case ShopButton.Shop_ButtonID.SUB_BAR:    		OBJ_SPECTRUM.SetActive(Save.GetBool(id + "Active")); 								break;
			case ShopButton.Shop_ButtonID.SUB_BK_SHADE: 	OBJ_BK_SHADE.SetActive(Save.GetBool(id + "Active")); 								break;
			case ShopButton.Shop_ButtonID.SUB_BK_DEFAULT: 	OBJ_BK_DEFAULT.SetActive(Save.GetBool(id + "Active"));								break;
		}
	}

	void Update ()
	{
	}

	public void Manage(ShopButton.Shop_ButtonID id, bool active)
	{
		switch (id)
		{
			case ShopButton.Shop_ButtonID.SUB_TRIANGLE: 	OBJ_TRIANGLE.SetActive(active); 								break;
			case ShopButton.Shop_ButtonID.SUB_SQUARE:		OBJ_SQUARE.SetActive(active);									break;
			case ShopButton.Shop_ButtonID.SUB_BLOOM: 		OBJ_CAMERA.GetComponent<BloomOptimized>().enabled = active; 	break;
			case ShopButton.Shop_ButtonID.SUB_FISHEYE: 		OBJ_CAMERA.GetComponent<Fisheye>().enabled = active; 			break;
			case ShopButton.Shop_ButtonID.SUB_CIRCLE:		OBJ_CIRCLE.SetActive(active); 									break;
			case ShopButton.Shop_ButtonID.SUB_BAR:     		OBJ_SPECTRUM.SetActive(active); 								break;
			case ShopButton.Shop_ButtonID.SUB_BK_SHADE: 	OBJ_BK_SHADE.SetActive(active); 								break;
			case ShopButton.Shop_ButtonID.SUB_BK_DEFAULT: 	OBJ_BK_DEFAULT.SetActive(active);								break;

		}
	}
}
