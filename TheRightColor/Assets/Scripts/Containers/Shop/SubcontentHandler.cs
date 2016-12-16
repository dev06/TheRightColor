using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SubcontentHandler : MonoBehaviour {

	private List<GameObject> _children;

	void Start ()
	{
		_children = new List<GameObject>();

		for (int i = 0; i < transform.childCount; i++)
		{
			_children.Add(transform.GetChild(i).gameObject);
		}

	}

	// Update is called once per frame
	void Update () {

	}
}
