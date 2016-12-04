using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShopPanel : MonoBehaviour {


	public Toggle toggle;
	public enum PanelID
	{
		Aesthetics,
		Features,
	}

	public PanelID panelID;

	void Start () {

	}

	void Update ()
	{
		transform.GetChild(0).gameObject.SetActive(toggle.isOn);
	}

	void ActivatePanel(PanelID panelID)
	{
		switch (panelID)
		{
			case PanelID.Aesthetics:
			{

				break;
			}
		}
	}
}
