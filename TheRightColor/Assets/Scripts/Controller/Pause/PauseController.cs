using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseController : MonoBehaviour {

	private Image _background;
	private float _hue;
	private Color _hueColor;
	void Start ()
	{
		_background = transform.FindChild("background").GetComponent<Image>();
	}

	void Update ()
	{
		if (GameManager.Instance.state == State.Pause)
		{
			ChangeBackGround();
		}
	}

	private void ChangeBackGround()
	{
		_hue = Mathf.PingPong(Time.time / 15.0f, 1.0f);
		_hueColor = Color.HSVToRGB(_hue, .5f, .5f);
		_background.color = _hueColor;
	}
}
