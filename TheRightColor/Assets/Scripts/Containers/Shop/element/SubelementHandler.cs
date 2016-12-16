using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class SubelementHandler : ShopButton {

	private ShopButton.Shop_ButtonID 							_tempID;// used for checking which button is press
	private GameObject 											_children;
	private bool 												_isParentActive;
	private Vector2 											_anchoredPosition;
	public Subelement 											_constructor;
	public ElementHandler										element;
	public Vector2 												_targetAnchoredPosition;
	private Text 												_name;
	private Image 												_activeImage;
	private Text 												_costText;
	private bool 												_cap;


	void OnEnable()
	{
		EventManager.OnDialogPositive += OnDialogPositive;
	}
	void OnDisable()
	{
		EventManager.OnDialogPositive -= OnDialogPositive;
		_cap = false;
		StopAllCoroutines();
	}

	void Start ()
	{
		_children = transform.parent.parent.gameObject;
		_anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
		_name = GetComponentInChildren<Text>();
		_activeImage = transform.FindChild("active").GetComponent<Image>();
		_costText = transform.FindChild("cost").GetComponent<Text>();
		_activeImage.enabled = false;
		Initialize();
	}

	private void Initialize()
	{
		if (_constructor != null)
		{
			_name.text = _constructor.name;
			buttonID = _constructor.id;
			gameObject.name = _constructor.name;
			_costText.text = _constructor.cost + "";

			try
			{
				_constructor.SetPurchased(Save.GetBool(buttonID + ""));
				_constructor.SetActive(Save.GetBool(buttonID + "Active"));

				_activeImage.enabled = _constructor.purchased;
				_activeImage.sprite = _constructor.active ? AppResources.Donut_Close : AppResources.Donut_Open;

			} catch (System.Exception e)
			{

			}
			_costText.enabled = !_activeImage.enabled;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (_cap == false)
		{
			if (_children.activeSelf)
			{

				StartCoroutine("Open");

				_cap = true;
			}

		}
	}

	IEnumerator Open()
	{
		Reset();
		while (Vector2.Distance(_targetAnchoredPosition,  _anchoredPosition) > .5f)
		{

			_anchoredPosition = Vector2.Lerp(_anchoredPosition, _targetAnchoredPosition, .15f);
			GetComponent<RectTransform>().anchoredPosition = _anchoredPosition;
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}



	private void Reset()
	{
		_anchoredPosition = Vector2.zero;
	}

	public void SetTargetPosition(Vector2 _pos)
	{
		_targetAnchoredPosition = _pos;
	}


	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);


		if (element != null)
		{
			if (element.childToggle)
			{
				for (int i = 0; i < element.children.Count; i++)
				{
					if (element.children[i].GetComponent<SubelementHandler>()._constructor.purchased)
					{
						element.children[i].GetComponent<SubelementHandler>().Toggle(false);
					}
				}
			}

		} else {
			Debug.Log("Element is null");
		}



		if (_constructor.purchased == false)
		{
			if (EventManager.OnSubelementPress != null)
			{
				EventManager.OnSubelementPress(buttonID);
				_tempID = buttonID;
			}
		} else {

			if (_activeImage != null)
			{
				if (_constructor.canToggle)
				{
					Toggle(!_constructor.active);

				}
			}
			//ShopController.Instance.Manage(buttonID, _constructor.active);
		}




	}

	public void Toggle(bool b)
	{
		_constructor.SetActive(b);
		_activeImage.enabled = true;
		_activeImage.sprite = _constructor.active ? AppResources.Donut_Close : AppResources.Donut_Open;
		_costText.enabled = !_activeImage.enabled;
		ShopController.Instance.Manage(buttonID, _constructor.active);
		Save.SetBool(buttonID + "", _constructor.purchased);
		Save.SetBool(buttonID + "Active", _constructor.active);
	}

	void OnDialogPositive()
	{
		if (_tempID == buttonID)
		{
			if (element != null)
			{
				if (element.childToggle)
				{
					for (int i = 0; i < element.children.Count; i++)
					{
						if (element.children[i].GetComponent<SubelementHandler>()._constructor.purchased)
						{
							element.children[i].GetComponent<SubelementHandler>().Toggle(false);
						}
					}
				}

			} else {
				Debug.Log("Element is null");
			}


			_constructor.SetPurchased(true);
			_constructor.SetActive(true);
			_activeImage.enabled = true;
			_activeImage.sprite =  AppResources.Donut_Close;
			Save.SetBool(buttonID + "", _constructor.purchased);
			Save.SetBool(buttonID + "Active", _constructor.active);
			_costText.enabled = !_activeImage.enabled;
			ShopController.Instance.Manage(_tempID, _constructor.active);

			_tempID = ShopButton.Shop_ButtonID.None;
		}
	}
}

public class Subelement
{
	public ShopButton.Shop_ButtonID id;
	public Subelement[] siblings;
	public string name;
	public int cost;
	public bool purchased;
	public bool active;
	public bool canToggle;

	public Subelement(string name, ShopButton.Shop_ButtonID id)
	{
		this.id = id;
		this.name = name;
	}

	public Subelement(string name, ShopButton.Shop_ButtonID id, bool canToggle)
	{
		this.id = id;
		this.name = name;
		this.canToggle = canToggle;

	}


	public Subelement(string name, ShopButton.Shop_ButtonID id, bool canToggle, int cost)
	{
		this.id = id;
		this.name = name;
		this.canToggle = canToggle;
		this.cost = cost;
	}

	public Subelement(string name, ShopButton.Shop_ButtonID id, bool canToggle, int cost, Subelement[] siblings)
	{
		this.id = id;
		this.name = name;
		this.canToggle = canToggle;
		this.cost = cost;
		this.siblings = siblings;
	}

	public void SetPurchased(bool b)
	{
		purchased = b;
	}

	public void SetActive(bool b)
	{
		active = b;
	}
}
