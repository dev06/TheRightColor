using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Notification_Dialog : MonoBehaviour {

	public bool onFirstLauch;
	public string dialogText;
	public Color dialogTextColor;

	private Text _text;


	void OnEnable()
	{
		EventManager.OnDialogConfirm += OnDialogConfirm;
	}

	void OnDisable()
	{
		EventManager.OnDialogConfirm -= OnDialogConfirm;
	}

	void Start ()
	{
		SetActive(true);
		if (onFirstLauch)
		{
			if (GameManager.Instance.firstLaunch)
			{
				_text = transform.FindChild("Text").GetComponent<Text>();
				_text.text = dialogText;
				_text.color = dialogTextColor;
			} else
			{
				SetActive(false);
			}
		}
	}


	void SetActive(bool active)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(active);
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnDialogConfirm()
	{
		SetActive(false);
	}
}
