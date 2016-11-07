using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UITheme : MonoBehaviour {

	public GameObject[] panelBackGround;
	public GameObject[] panelWorldImage;

	void Start ()
	{
		panelBackGround = GameObject.FindGameObjectsWithTag("Panel/BackGround");
		panelWorldImage = GameObject.FindGameObjectsWithTag("Panel/WorldImage");

		SetTheme();
	}

	void SetTheme()
	{
		for (int i = 0; i < panelBackGround.Length; i++)
		{
			panelBackGround[i].GetComponent<RectTransform>().localScale = new Vector3(MasterVar.Panel_BackGroundScale, MasterVar.Panel_BackGroundScale, MasterVar.Panel_BackGroundScale);

		}

		for (int i = 0; i < panelWorldImage.Length; i++)
		{
			panelWorldImage[i].GetComponent<Image>().color = MasterVar.Panel_WorldImageColor;

		}

	}

	void Update ()
	{

	}
}
