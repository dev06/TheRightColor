using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InteractiveTileOverlay : MonoBehaviour {

	private Vector3 _scale;
	private Image _overlay;
	private Sprite _sprite;
	private Color _color;
	private float _alpha;
	private Vector3 _targetScale;
	private float _velocity;

	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;

	}

	void Start ()
	{
		transform.localScale = new Vector3(.5f, .5f, .5f);
		_sprite = transform.parent.transform.GetComponent<Image>().sprite;
		_overlay = GetComponent<Image>();
		_overlay.sprite = _sprite;
		_color = GetComponent<Image>().color;
		_alpha = 0.3f;
		_targetScale = new Vector3(1f, 1f, 1f);
		_velocity = 2.0f;
	}

	void Update ()
	{
		if (_scale.x < .97f)
		{
			_scale = Vector3.Lerp(_scale, _targetScale, Time.deltaTime * _velocity);
			transform.localScale = _scale;
			_alpha -= Time.deltaTime * (_velocity / 10.0f);
			_color = new Color(_color.r, _color.g, _color.b, _alpha);
			_overlay.color = _color;

		} else {
			Reset();
		}
	}

	private void Reset()
	{
		_scale =  new Vector3(.5f, .5f, .5f);
		_alpha = .3f;

	}


	void OnCorrectColor()
	{
		Reset();
	}


}
