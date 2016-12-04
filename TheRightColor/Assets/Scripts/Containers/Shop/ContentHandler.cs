using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class ContentHandler : MonoBehaviour {



	public static ContentHandler Instance;

	public ContentCreator contentCreator;

	public List<ElementHandler> elements;

	public ContentCreator.ContentType contentType;

	void Start()
	{
		Instance = this;

		elements = new List<ElementHandler>();


		contentCreator = new ContentCreator(contentType, transform);

		for (int i = 0; i < transform.childCount; i++)
		{
			elements.Add(transform.GetChild(i).GetComponent<ElementHandler>());
		}

		StartCoroutine("CalcuateOffsetValue");
	}


	// go through child count
	// get sublement of each childcount
	// set the offet of next elemt to sublement childcount
	IEnumerator  CalcuateOffsetValue()
	{
		yield return new WaitForSeconds(.4f);
		for (int i = 0; i < elements.Count; i++)
		{
			if (i >= 0)
			{
				int subelementCount = elements[i].subelementCount;
				if (i < elements.Count - 1)
				{
					elements[i + 1].prevSubelementCount = subelementCount;
					elements[i + 1].previousElement = elements[i];
				}
			}
		}
	}

	void Update()
	{

	}
}
