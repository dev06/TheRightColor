using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class ElementHandler : ShopButton {

	public List<SubelementHandler> subelements;
	public RectTransform rectTransform;
	public GameObject _subElementPrefab;
	public GameObject children;
	public bool isOpen;
	public int childIndex;
	public float width;
	public float height;
	public float childrenWidth;
	public float childrenHeight;


	public ElementHandler previousElement;
	public Element constructor;

	public int subelementCount;
	public int prevSubelementCount;


	private Text _elementName;
	private Toggle _toggle;
	private float _paddingTop = 20.0f;



	void Start ()
	{
		_subElementPrefab = (GameObject)Resources.Load("Prefabs/UI/Shop/Content/subelement");
		subelements = new List<SubelementHandler>();
		children = transform.FindChild("children").FindChild("subcontent").gameObject;




		childIndex = transform.GetSiblingIndex();
		_elementName = transform.FindChild("name").GetComponent<Text>();
		_toggle = GetComponent<Toggle>();
		rectTransform = GetComponent<RectTransform>();
		width = rectTransform.rect.width;
		height = rectTransform.rect.height;
		Initialize();

	}


	private void Initialize()
	{
		if (constructor != null)
		{
			SetText(constructor.name);
		}

		for (int i = 0; i <  constructor.subelements; i++)
		{
			GameObject subelement = (GameObject)Instantiate(_subElementPrefab);
			RectTransform rt = subelement.GetComponent<RectTransform>();
			subelement.transform.SetParent(children.transform);

			rt.localScale = new Vector3(1, 1, 1);
			rt.localPosition = new Vector3(0, 0, 0);
			rt.anchorMin = new Vector2(0, 0);
			rt.anchorMax = new Vector2(1, 1);
			rt.offsetMax = new Vector2(-100, 0);
			rt.offsetMin = new Vector2(100, 0);
			childrenHeight = rt.rect.height;
			rt.GetComponent<SubelementHandler>().SetTargetPosition(new Vector3(0, -i * childrenHeight * 1.2f, 0));

			subelementCount = i + 2;
		}
		children.GetComponent<RectTransform>().offsetMin = new Vector2(0, children.GetComponent<RectTransform>().offsetMin.y - _paddingTop);

	}

	// Update is called once per frame
	void Update ()
	{

		isOpen = _toggle.isOn;
		children.SetActive(_toggle.isOn);

		if (GameManager.Instance.state == State.Control)
		{
			if (previousElement != null)
			{
				if (previousElement.isOpen)
				{
					// this is the last subelement position in the previouselement
					RectTransform lastSublement = previousElement.children.transform.
					                              GetChild(previousElement.children.transform.childCount - 1).
					                              GetComponent<RectTransform>();

					rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition,
					                                 new Vector3(0, ((lastSublement.anchoredPosition.y - (childIndex * previousElement.height) - 100) * 1.1f), 0),
					                                 .2f);

				} else
				{

					rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, previousElement.rectTransform.anchoredPosition +
					                                 new Vector2(0, -previousElement.height * 1.1f), .2f);
				}
			}
		}

	}




	void SetText(string text)
	{
		_elementName.text = text;
	}

	public GameObject GetChild(int index)
	{
		return children.transform.GetChild(index).gameObject;
	}

	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
	}
}

public class Element
{
	public ShopButton.Shop_ButtonID id;
	public string name;
	public int subelements;

	public Element(string name, int subelements, ShopButton.Shop_ButtonID id)
	{
		this.name = name;
		this.subelements = subelements;
		this.id = id;
	}
}
