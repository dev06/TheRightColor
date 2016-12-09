using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SubelementHandler : ShopButton {

	private ShopButton.Shop_ButtonID tempID;// used for checking which button is press
	private GameObject _children;
	private bool _isParentActive;
	private Vector2 _anchoredPosition;
	public Subelement constructor;
	public Vector2 _targetAnchoredPosition;
	private Text _name;
	private Image _activeImage;
	private Text _costText;
	private bool cap;


	void OnEnable()
	{
		EventManager.OnDialogPositive += OnDialogPositive;
	}
	void OnDisable()
	{
		EventManager.OnDialogPositive -= OnDialogPositive;
		cap = false;
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

		ShopController.Instance.Manage(buttonID, constructor.active);

	}

	private void Initialize()
	{
		if (constructor != null)
		{
			_name.text = constructor.name;
			buttonID = constructor.id;
			gameObject.name = constructor.name;
			_costText.text = constructor.cost + "";

			try
			{
				constructor.SetPurchased(Save.GetBool(buttonID + ""));
				constructor.SetActive(Save.GetBool(buttonID + "Active"));

				_activeImage.enabled = constructor.purchased;
				_activeImage.sprite = constructor.active ? AppResources.Donut_Close : AppResources.Donut_Open;

			} catch (System.Exception e)
			{

			}
			_costText.enabled = !_activeImage.enabled;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (cap == false)
		{
			if (_children.activeSelf)
			{

				StartCoroutine("Open");

				cap = true;
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

		if (constructor.purchased == false)
		{
			if (EventManager.OnSubelementPress != null)
			{
				EventManager.OnSubelementPress(buttonID);
				tempID = buttonID;
			}
		} else {

			if (_activeImage != null)
			{
				if (constructor.canToggle)
				{
					constructor.SetActive(!constructor.active);
					_activeImage.enabled = true;
					_activeImage.sprite = constructor.active ? AppResources.Donut_Close : AppResources.Donut_Open;
					_costText.enabled = !_activeImage.enabled;
				}
			}


			ShopController.Instance.Manage(buttonID, constructor.active);

		}


		Save.SetBool(buttonID + "", constructor.purchased);
		Save.SetBool(buttonID + "Active", constructor.active);
	}

	void OnDialogPositive()
	{
		if (tempID == buttonID)
		{
			constructor.SetPurchased(true);
			constructor.SetActive(true);
			_activeImage.enabled = true;
			_activeImage.sprite =  AppResources.Donut_Close;
			Save.SetBool(buttonID + "", constructor.purchased);
			Save.SetBool(buttonID + "Active", constructor.active);
			_costText.enabled = !_activeImage.enabled;
			ShopController.Instance.Manage(tempID, constructor.active);

			tempID = ShopButton.Shop_ButtonID.None;
		}
	}
}

public class Subelement
{
	public ShopButton.Shop_ButtonID id;
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

	public void SetPurchased(bool b)
	{
		purchased = b;
	}

	public void SetActive(bool b)
	{
		active = b;
	}
}
