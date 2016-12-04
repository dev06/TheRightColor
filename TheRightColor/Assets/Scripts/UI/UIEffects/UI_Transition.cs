using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_Transition : MonoBehaviour {

	public enum TransitionType
	{
		WIDTH,
	}

	public enum TransitionSpeed
	{
		Slow,
		Medium,
		Fast,
	}

	public TransitionType _transitionType;
	public TransitionSpeed _transitionSpeed;
	private RectTransform _rt;
	private float _speed;
	private Image _image;
	private Vector3 _scale;


	public bool start;

	void Start ()
	{
		_rt = GetComponent<RectTransform>();
		_image = GetComponent<Image>();
		_scale = _rt.localScale;
		switch (_transitionSpeed) {
			case TransitionSpeed.Slow: _speed = 0.1f; break;
			case TransitionSpeed.Medium: _speed = 0.3f; break;
			case TransitionSpeed.Fast: _speed = 0.5f; break;

		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void OpenBox()
	{
		switch (_transitionType)
		{
			case TransitionType.WIDTH:
			{
				StartCoroutine("Open", 1f);
				break;
			}
		}
	}



	IEnumerator Open(float target )
	{
		if (_rt == null)
		{
			_rt = GetComponent<RectTransform>();
		}
		else
		{
			_scale.x = 0;
			while (Mathf.Abs(_scale.x - target) > 0.02f)
			{
				float _scaleX = _scale.x;

				_scaleX = Mathf.Lerp(_scaleX, target,   _speed / target);
				_scale.x = _scaleX;
				_rt.localScale = _scale;
				yield return new WaitForSeconds(Time.deltaTime);
			}
		}
	}
}
