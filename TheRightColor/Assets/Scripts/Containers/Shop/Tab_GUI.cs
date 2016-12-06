using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class Tab_GUI : MonoBehaviour {

	private List<GameObject> tabs;

	void Start ()
	{
		tabs = new List<GameObject>();

		// for (int i = 0; i < 2; i++)
		// {
		// 	GameObject tab = (GameObject)Instantiate(AppResources.Tab, Vector3.zero, Quaternion.identity);
		// 	tab.transform.SetParent(GameObject.FindWithTag("Shop/Tabs").transform);
		// 	tab.transform.localScale = new Vector3(1, 1, 1);
		// }


		//populates tabs lists
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).gameObject.name.Contains("tab"))
			{
				tabs.Add(transform.GetChild(i).gameObject);
			}
		}

		Setup();
	}

	/// <summary>
	/// Sets up the tabs zones
	/// </summary>

	private void Setup()
	{
		for (int i = 0; i < tabs.Count; i++)
		{
			GameObject tab = tabs[i];
			RectTransform tr = tab.GetComponent<RectTransform>();
			float sy = tr.sizeDelta.y;
			float sx = GetComponent<RectTransform>().sizeDelta.x / tabs.Count;
			tr.sizeDelta = new Vector2(sx, sy);

			tr.anchoredPosition = new Vector3(i * sx, 0, tr.position.z);
			Debug.Log(tr.offsetMin);
		}
	}


	void Update () {

	}
}
