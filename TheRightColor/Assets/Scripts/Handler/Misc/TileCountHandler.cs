using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TileCountHandler : MonoBehaviour {

	private Text _text;

	void Start ()
	{
		_text = GetComponent<Text>();
	}

	void Update()
	{
		_text.text = GameManager.Instance.tileCount + "";

	}
}
