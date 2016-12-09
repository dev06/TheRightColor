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

	private bool activate;

	void Start () {
		StartCoroutine("StartActivate");
	}

	IEnumerator StartActivate()
	{
		yield return new WaitForSeconds(.5f);
		activate = true;
	}


	void Update ()
	{
		if (activate) {
			transform.GetChild(0).gameObject.SetActive(toggle.isOn);
		}
	}

}
