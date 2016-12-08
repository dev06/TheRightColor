using UnityEngine;
using System.Collections;

public class InteractiveTileContainer : MonoBehaviour {

	private RectTransform rt;
	void Start ()
	{
		rt = GetComponent<RectTransform>();

	}

	void Update ()
	{

	}

	IEnumerator OpenIncorrect()
	{
		yield return null;

	}
}
