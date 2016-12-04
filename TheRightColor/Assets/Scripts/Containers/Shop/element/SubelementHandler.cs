using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SubelementHandler : MonoBehaviour {

	private GameObject _children;
	private bool _isParentActive;
	private Vector2 _anchoredPosition;
	public Vector2 _targetAnchoredPosition;

	bool cap;
	void OnEnable()
	{
		ShopButton.OnShopButtonClick += OnShopButtonClick;


	}
	void OnDisable()
	{
		ShopButton.OnShopButtonClick -= OnShopButtonClick;
		cap = false;

	}

	void Start ()
	{
		_children = transform.parent.parent.gameObject;
		_anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

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

	void OnShopButtonClick(ShopButton.Shop_ButtonID id)
	{


	}
}
