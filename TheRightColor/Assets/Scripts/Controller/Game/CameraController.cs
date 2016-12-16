using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private float _hue;
	private Color _hueColor = new Color();
	private Camera _camera;
	void Start () {
		_camera = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		ChangeBackGround();
	}

	private void ChangeBackGround()
	{
		_hue = Mathf.PingPong(Time.time / 15.0f, 1.0f);
		_hueColor = Color.HSVToRGB(_hue, .5f, .5f);
		_camera.backgroundColor = _hueColor;
	}

}
