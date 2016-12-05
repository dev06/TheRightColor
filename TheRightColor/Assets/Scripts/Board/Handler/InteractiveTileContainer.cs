using UnityEngine;
using System.Collections;

public class InteractiveTileContainer : MonoBehaviour {

	private RectTransform rt;
	void Start ()
	{
		rt = GetComponent<RectTransform>();

		Debug.Log(rt.offsetMax);
	}

	void Update ()
	{

	}

	IEnumerator OpenIncorrect()
	{
		yield return null;

	}
}
