using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ShopController : MonoBehaviour {

	public static ShopController Instance;

	private GameObject OBJ_EFFECTS;
	private GameObject OBJ_TRIANGLE;
	private GameObject OBJ_SQUARE;

	void Awake()
	{
		Instance = this;
	}

	void Start ()
	{
		OBJ_EFFECTS = GameObject.FindWithTag("Effects").gameObject;
		OBJ_TRIANGLE = OBJ_EFFECTS.transform.FindChild("Triangle").gameObject;
		OBJ_SQUARE = OBJ_EFFECTS.transform.FindChild("Square").gameObject;

	}

	void Update ()
	{
	}

	public void Manage(ShopButton.Shop_ButtonID id, bool active)
	{
		switch (id)
		{
			case ShopButton.Shop_ButtonID.SUB_TRIANGLE:
			{
				OBJ_TRIANGLE.SetActive(active);
				break;
			}
			case ShopButton.Shop_ButtonID.SUB_SQUARE:
			{
				OBJ_SQUARE.SetActive(active);
				break;
			}
		}
	}
}
