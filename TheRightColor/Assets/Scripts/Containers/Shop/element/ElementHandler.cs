using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class ElementHandler : ShopButton {

	public List<SubelementHandler> 	subelements;
	public ElementHandler 			previousElement;
	public Element 					constructor;
	public ContentCreator.SUBCAT    subcategory;
	public RectTransform 			rectTransform;
	public GameObject 				subElementPrefab;
	public GameObject 				subcontent;
	public List<GameObject>         children;
	public bool 					isOpen;
	public bool 					childToggle;
	public int 						childIndex;
	public int 						subelementCount;
	public int 						prevSubelementCount;
	public float 					width;
	public float 					height;
	public float 					childrenWidth;
	public float 					childrenHeight;


	private float 					_elementHeight = 150f;
	private float 					_paddingTop = 1.2f;
	private float 					_paddingBetweenSubElements = 1.2f;
	private Text 					_elementName;
	private Toggle 					_toggle;

	private Vector3 				_openTargetVector;
	private Vector2 				_closeTargetVector;




	void OnDisable()
	{
		rectTransform.anchoredPosition = Vector2.zero;
	}

	void Start ()
	{
		subElementPrefab = (GameObject)Resources.Load("Prefabs/UI/Shop/Content/subelement");
		subelements = new List<SubelementHandler>();
		subcontent = transform.FindChild("children").FindChild("subcontent").gameObject;
		children = new List<GameObject>();
		childIndex = transform.GetSiblingIndex();
		_elementName = transform.FindChild("name").GetComponent<Text>();
		_toggle = GetComponent<Toggle>();
		rectTransform = GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, _elementHeight);
		width = rectTransform.rect.width;
		height = rectTransform.rect.height;
		Initialize();
	}


	private void Initialize()
	{
		if (constructor != null)
		{
			SetText(constructor.name);
			gameObject.name = constructor.name;
		}

		this.subcategory = constructor.subcategory;
		this.buttonID = constructor.id;


		for (int i = 0; i <  constructor.subelements; i++)
		{
			GameObject subelement = (GameObject)Instantiate(subElementPrefab);

			RectTransform rt = subelement.GetComponent<RectTransform>();
			subelement.transform.SetParent(subcontent.transform);
			Subelement s = new Subelement(subcategory.type[i],
			                              subcategory.GetButtonID(subcategory.type[i]),
			                              subcategory.GetButtonToggle(subcategory.type[i]),
			                              subcategory.GetButtonCost(subcategory.type[i]));
			rt.localScale = new Vector3(1, 1, 1);
			rt.localPosition = new Vector3(0, 0, 0);
			rt.anchorMin = new Vector2(0, 0);
			rt.anchorMax = new Vector2(1, 1);
			rt.offsetMax = new Vector2(-_elementHeight, 0);
			rt.offsetMin = new Vector2(_elementHeight, 0);
			childrenHeight = rt.rect.height;
			rt.GetComponent<SubelementHandler>()._constructor = s;
			rt.GetComponent<SubelementHandler>().element = this;
			rt.GetComponent<SubelementHandler>().SetTargetPosition(new Vector3(0,
			        (-i * childrenHeight * _paddingBetweenSubElements) - (childrenHeight * (_paddingTop - 1.0f)), 0));

			subelementCount = i + 2;

		}

		subcontent.GetComponent<RectTransform>().offsetMin = new Vector2(0, subcontent.GetComponent<RectTransform>().offsetMin.y);
		_openTargetVector = Vector3.zero;
		_closeTargetVector = Vector2.zero;


		for (int i = 0 ; i < subcontent.transform.childCount; i++)
		{
			children.Add(subcontent.transform.GetChild(i).gameObject);
		}



		InitChildToggle(buttonID);
	}


	void InitChildToggle(ShopButton.Shop_ButtonID id)
	{
		switch (id)
		{
			case ShopButton.Shop_ButtonID.ELE_Particles: childToggle = true; break;

		}
	}

	// Update is called once per frame

	void FixedUpdate ()
	{

		isOpen = _toggle.isOn;
		subcontent.SetActive(_toggle.isOn);

		if (GameManager.Instance.state == State.Control)
		{

			if (previousElement != null)
			{
				if (previousElement.isOpen)
				{
					// this is the last subelement position in the previouselement
					RectTransform lastSublement = previousElement.subcontent.transform.
					                              GetChild(previousElement.subcontent.transform.childCount - 1).
					                              GetComponent<RectTransform>();

					_openTargetVector.x = 0;

					_openTargetVector.y = (lastSublement.anchoredPosition.y - (childIndex * previousElement.height) - _elementHeight)
					                      - (_paddingTop - 1.0f) * _elementHeight;

					_openTargetVector.z = 0;

					rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition,
					                                 _openTargetVector,
					                                 .2f);

				} else
				{
					_closeTargetVector.x = 0;
					_closeTargetVector.y = -previousElement.height;
					rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition,
					                                 previousElement.rectTransform.anchoredPosition +
					                                 _closeTargetVector, .2f);
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
		return subcontent.transform.GetChild(index).gameObject;
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
	public ContentCreator.SUBCAT subcategory;
	public Element(string name, int subelements, ShopButton.Shop_ButtonID id, ContentCreator.SUBCAT subcategory)
	{
		this.name = name;
		this.subelements = subelements;
		this.id = id;
		this.subcategory = subcategory;
	}
}
