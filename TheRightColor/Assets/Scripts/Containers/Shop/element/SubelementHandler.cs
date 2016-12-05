using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SubelementHandler : ShopButton {

	private GameObject _children;
	private bool _isParentActive;
	private Vector2 _anchoredPosition;
	public Subelement constructor;
	public Vector2 _targetAnchoredPosition;
	private Text _name;
	bool cap;
	void OnEnable()
	{


	}
	void OnDisable()
	{
		cap = false;

	}

	void Start ()
	{
		_children = transform.parent.parent.gameObject;
		_anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
		_name = GetComponentInChildren<Text>();
		Initialize();

	}

	private void Initialize()
	{
		if (constructor != null)
		{
			_name.text = constructor.name;
			buttonID = constructor.id;
			gameObject.name = constructor.name;
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

		while (Vector2.Distance(_targetAnchoredPosition,  _anchoredPosition) > .1f)
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
		Debug.Log(buttonID);

	}
}

public class Subelement
{
	public ShopButton.Shop_ButtonID id;
	public string name;

	public Subelement(string name, ShopButton.Shop_ButtonID id)
	{
		this.id = id;
		this.name = name;
	}
}
