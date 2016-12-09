using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ContentCreator: MonoBehaviour {

	private GameObject elementPrefab;

	public class SUBCAT
	{
		private const string Triangles = "Triangles";
		private const string Circles = "Circles";
		private const string Squares = "Squares";
		private const string Bar = "Bar";
		private const string BackgroundDefault = "Default";
		private const string Bloom = "Bloom";
		private const string Fisheye = "Fisheye";

		private const string Music_AlanWalker_Fade = "Fade by Alan Walker";
		private const string PauseButton = "Buy a Pause Button";
		private const string SaveScore = "Ability to save the score";



		//AESTHETICS
		public static string [] PARTICLE = { Triangles, Circles , Squares};
		public static string [] SPECTRUM = { Bar};
		public static string [] BACKGROUND = { BackgroundDefault};
		public static string [] EFFECTS = {Bloom, Fisheye};


		//FEATURES
		public static string [] MUSIC = { Music_AlanWalker_Fade, "Hello", "Truman", "show"};
		public static string [] PAUSE = { PauseButton};
		public static string [] SAVESCORE = { SaveScore};


		public string [] type;

		public SUBCAT(string[] type)
		{
			this.type = type;
		}

		public ShopButton.Shop_ButtonID GetButtonID(string name)
		{
			switch (name)
			{
				//AESTHETICS
				case Triangles: return ShopButton.Shop_ButtonID.SUB_TRIANGLE;
				case Circles: return ShopButton.Shop_ButtonID.SUB_CIRCLE;
				case Squares: return ShopButton.Shop_ButtonID.SUB_SQUARE;
				case Bar: return ShopButton.Shop_ButtonID.SUB_BAR;
				case BackgroundDefault: return ShopButton.Shop_ButtonID.SUB_BK_DEFAULT;
				case Bloom : return ShopButton.Shop_ButtonID.SUB_BLOOM;
				case Fisheye: return ShopButton.Shop_ButtonID.SUB_FISHEYE;

				//FEATURES
				case Music_AlanWalker_Fade: return ShopButton.Shop_ButtonID.SUB_MUSIC_FADE;
				case PauseButton: return ShopButton.Shop_ButtonID.SUB_PAUSE;
				case SaveScore: return ShopButton.Shop_ButtonID.SUB_SAVESCORE;


			}
			return ShopButton.Shop_ButtonID.None;
		}

		public bool GetButtonToggle(string name)
		{
			switch (name)
			{
				case PauseButton: return false;
				case SaveScore: return false;
			}
			return true;
		}

		public int GetButtonCost(string name)
		{
			switch (name)
			{

				case Triangles: return MasterVar.COST_SUB_TRIANGLE;
				case Circles: return MasterVar.COST_SUB_CIRCLE;
				case Squares: return MasterVar.COST_SUB_SQUARE;

				//==============================================================================================================//

				case Bar: return MasterVar.COST_SUB_BAR;

				//==============================================================================================================//

				case BackgroundDefault: return MasterVar.COST_SUB_DEFAULT;

				//==============================================================================================================//

				case Bloom : return MasterVar.COST_SUB_BLOOM;
				case Fisheye: return MasterVar.COST_SUB_FISHEYE;

				//==============================================================================================================//

				case Music_AlanWalker_Fade: return MasterVar.COST_SUB_TRIANGLE;
				case PauseButton: return MasterVar.COST_SUB_TRIANGLE;
				case SaveScore: return MasterVar.COST_SUB_TRIANGLE;


			}
			return -1;
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
				CreateElement("Particles", SUBCAT.PARTICLE.Length, ShopButton.Shop_ButtonID.ELE_Particles, parent, new SUBCAT(SUBCAT.PARTICLE));
				CreateElement("Spectrum", SUBCAT.SPECTRUM.Length, ShopButton.Shop_ButtonID.ELE_SPECTRUM, parent, new SUBCAT(SUBCAT.SPECTRUM));
				CreateElement("Background", SUBCAT.BACKGROUND.Length, ShopButton.Shop_ButtonID.ELE_BACKGROUND, parent, new SUBCAT(SUBCAT.BACKGROUND));
				CreateElement("Effects", SUBCAT.EFFECTS.Length, ShopButton.Shop_ButtonID.ELE_EFFECTS, parent, new SUBCAT(SUBCAT.EFFECTS));

				break;
			}
			case ContentType.Features:
			{
				CreateElement("Music", SUBCAT.MUSIC.Length, ShopButton.Shop_ButtonID.ELE_MUSIC, parent, new SUBCAT(SUBCAT.MUSIC));
				CreateElement("Pause Button", SUBCAT.PAUSE.Length, ShopButton.Shop_ButtonID.ELE_PAUSE, parent, new SUBCAT(SUBCAT.PAUSE));
				CreateElement("Save Score", SUBCAT.SAVESCORE.Length, ShopButton.Shop_ButtonID.ELE_SAVE, parent, new SUBCAT(SUBCAT.SAVESCORE));
				// CreateElement("Features", 4, ShopButton.Shop_ButtonID.Particles, parent);
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
	private void CreateElement(string name, int subelementCount, ShopButton.Shop_ButtonID id, Transform parent, SUBCAT subcategory)
	{
		Element e = new Element(name, subelementCount, id, subcategory);
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

