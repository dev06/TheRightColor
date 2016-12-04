using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ContentCreator: MonoBehaviour {

	private GameObject elementPrefab;

	private class SUBCAT
	{
		public enum SUBCAT_PARTICLE
		{
			Triangle,
			Square,
			Circle,
		}

		public enum SUBCAT_SPECTRUM
		{
			Bar,
		}

		public enum SUBCAT_BACKGROUND
		{
			Default,
		}
	}


	public enum ContentType
	{
		None,
		Aesthetics,
		Features,
	}

	public ContentType type;
	public Transform parent;

	public ContentCreator(ContentType type, Transform parent)
	{
		this.type = type;
		this.parent = parent;
		elementPrefab = (GameObject)Resources.Load("Prefabs/UI/Shop/Content/element");

		CreateContent(type);
	}

	/// <summary>
	/// Creates the content for a tab
	/// </summary>
	/// <param name="type"></param>
	private void CreateContent(ContentType type)
	{
		switch (type)
		{
			case ContentType.Aesthetics:
			{
				CreateElement("Particles", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Audio Specturm", 2, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Background", 3, ShopButton.Shop_ButtonID.Particles, parent);

				break;
			}
			case ContentType.Features:
			{
				CreateElement("Audio", 4, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Pause Button", 1, ShopButton.Shop_ButtonID.Particles, parent);
				CreateElement("Save Score", 1, ShopButton.Shop_ButtonID.Particles, parent);
				//CreateElement("Features", 4, ShopButton.Shop_ButtonID.Particles, parent);
				break;
			}
		}
	}

	/// <summary>
	/// Creates new Graphic of the elements in shop tab
	/// </summary>
	/// <param name="name"></param>
	/// <param name="subelementCount"></param>
	/// <param name="id"></param>
	private void CreateElement(string name, int subelementCount, ShopButton.Shop_ButtonID id, Transform parent)
	{
		Element e = new Element(name, subelementCount, id);
		GameObject clone = (GameObject)Instantiate(elementPrefab);
		clone.transform.SetParent(parent.transform);
		RectTransform rt = clone.GetComponent<RectTransform>();
		rt.localScale = new Vector3(1, 1, 1);
		rt.localPosition = new Vector3(0, 0, 0);

		rt.offsetMax = new Vector2(0, 100);
		rt.offsetMin = new Vector2(0, 0);
		rt.anchoredPosition = new Vector2(0, 0);
		clone.GetComponent<Toggle>().group = parent.GetComponent <ToggleGroup>();
		clone.GetComponent<ElementHandler>().constructor = e;
	}
}

