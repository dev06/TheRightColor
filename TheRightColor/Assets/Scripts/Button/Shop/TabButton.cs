using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TabButton : ShopButton {

	private GameObject _activeObj;
	private Toggle _toggle;
	private Image _activeObjImg;
	void Start ()
	{
		_activeObj = transform.FindChild("active").gameObject;
		_toggle =  GetComponent<Toggle>();
		_activeObjImg = _activeObj.GetComponent<Image>();

		StartCoroutine("OpenBox");
	}

	IEnumerator OpenBox()
	{
		yield return new WaitForSeconds(.1f);
		if (_toggle.isOn)
		{
			_activeObj.GetComponent<UI_Transition>().OpenBox();
		}
	}

	void FixedUpdate ()
	{
		_activeObjImg.enabled = _toggle.isOn;
	}

	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
		if (_activeObj != null)
		{
			_activeObj.GetComponent<UI_Transition>().OpenBox();
		}

	}
}
