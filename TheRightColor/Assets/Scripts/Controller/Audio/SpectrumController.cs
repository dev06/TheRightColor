using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class SpectrumController : MonoBehaviour {


	private AudioSource _audioSource;
	private int _barCount;
	private float _pitchVel;
	private float vel;
	private float[] spectrum = new float[256];
	private Transform _barContainer;
	private RectTransform _barContainerRT;
	private List<AnimatedObject> _animatedObject;
	private Slider _volume;



	void OnEnable()
	{
		EventManager.OnGameStateActive += OnGameStateActive;
		EventManager.OnPauseStateActive += OnPauseStateActive;
		EventManager.OnRetryButtonPress += OnRetryButtonPress;
	}


	void OnDisable()
	{
		EventManager.OnGameStateActive -= OnGameStateActive;
		EventManager.OnPauseStateActive -= OnPauseStateActive;
		EventManager.OnRetryButtonPress -= OnRetryButtonPress;
	}

	void Start ()
	{
		_barCount = 6;
		_barContainer = transform.GetChild(0);
		_barContainerRT = _barContainer.GetComponent<RectTransform>();

		_audioSource = GetComponent<AudioSource>();
		_animatedObject = new List<AnimatedObject>();

		GameObject _interactiveContainer = GameObject.FindWithTag("Container/InteractiveContainer");
		GameObject _generatorTile = GameObject.FindWithTag("Tile/GeneratorTile");
		GameObject _buttonContainer = GameObject.FindWithTag("Container/ButtonContainer");
		GameObject _score = GameObject.FindWithTag("Board/Score");
		GameObject _controlContainer = GameObject.FindWithTag("Container/ControlContainer");
		GameObject _creditContainer = GameObject.FindWithTag("Container/CreditContainer");
		GameObject _settingContainer = GameObject.FindWithTag("Container/SettingContainer");
		GameObject _challengeContainer = GameObject.FindWithTag("Container/SettingContainer");
		GameObject _effects = GameObject.FindWithTag("Effects");

		_animatedObject.Add(new AnimatedObject(_interactiveContainer, _interactiveContainer.transform.localScale));
		_animatedObject.Add(new AnimatedObject(_generatorTile, _generatorTile.transform.localScale));
		_animatedObject.Add(new AnimatedObject(_buttonContainer, _buttonContainer.transform.localScale));
		_animatedObject.Add(new AnimatedObject(_score, _score.transform.localScale, 10.0f));
		_animatedObject.Add(new AnimatedObject(_controlContainer, _controlContainer.transform.localScale));
		_animatedObject.Add(new AnimatedObject(_creditContainer, _creditContainer.transform.localScale));
		_animatedObject.Add(new AnimatedObject(_settingContainer, _settingContainer.transform.localScale));
		_animatedObject.Add(new AnimatedObject(_challengeContainer, _challengeContainer.transform.localScale, 3.0f));
		_animatedObject.Add(new AnimatedObject(_effects, _effects.transform.localScale, 2.0f));

		_volume = GameObject.FindWithTag("SettingOption/Volume").GetComponent<Slider>();
		PopulateBars();
		UpdateAudioSetting();
		_audioSource.time = Random.Range(0, _audioSource.clip.length / 4.0f);
	}

	void PopulateBars()
	{
		for (int i = 0; i < _barCount; i++)
		{
			GameObject _bar = (GameObject)Instantiate(AppResources.AudioSpecturmBar, Vector3.zero, Quaternion.identity);
			_bar.transform.SetParent(_barContainer);
			RectTransform _rectTransform = _bar.GetComponent<RectTransform>();
			_rectTransform.localScale = new Vector3(1, 1, 1);
			_rectTransform.sizeDelta = new Vector3(_barContainerRT.sizeDelta.x / _barCount, _barContainerRT.sizeDelta.y / _barCount, 1);
			_rectTransform.localPosition = new Vector3(i * _rectTransform.sizeDelta.x, 0, 0);
		}
	}


	void UpdateTileEntities(float spectrum, GameObject _object, Vector3 _objectBaseScale, float _velX, float _velY, float _intensity)
	{

		float _scaleX = _object.transform.localScale.x;
		float _scaleY = _object.transform.localScale.y;

		_scaleY = Mathf.SmoothDamp(_scaleY, _objectBaseScale.y + spectrum * ((_objectBaseScale.y * _intensity) / 10.0f) , ref _velX, .08f);
		_scaleX = Mathf.SmoothDamp(_scaleX, _objectBaseScale.x + spectrum * ((_objectBaseScale.x * _intensity) / 10.0f) , ref _velY, .08f);

		_object.transform.localScale = new Vector3( _scaleX, _scaleY, 1);

	}

	void OnGameStateActive()
	{
		_audioSource.UnPause();
	}

	void OnPauseStateActive()
	{
		_audioSource.Pause();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (GameManager.Instance.state != State.Pause)
		{
			if (GameManager.Instance.state != State.Game)
			{
				_audioSource.pitch = Mathf.SmoothDamp(_audioSource.pitch, MasterVar.AudioSecondaryPitch, ref _pitchVel, MasterVar.AudioPitchChangeRate);
			} else {
				_audioSource.pitch = Mathf.SmoothDamp(_audioSource.pitch, MasterVar.AudioDefaultPitch, ref _pitchVel, MasterVar.AudioPitchChangeRate);

			}

			AudioListener.GetSpectrumData( spectrum, 0, FFTWindow.Rectangular );

			for (int i = 0; i < _barContainer.childCount; i++)
			{
				RectTransform _rectTransform = _barContainer.GetChild(i).GetComponent<RectTransform>();
				float _scaleY = _rectTransform.localScale.y;
				_scaleY = Mathf.SmoothDamp(_scaleY, spectrum[i] * 10, ref vel, .12f);
				if (_scaleY < 0) { _scaleY = 0; }
				_rectTransform.localScale = new Vector3(1, _scaleY, 1);

			}
			for (int ii = 0; ii < _animatedObject.Count; ii++)
			{
				UpdateTileEntities(spectrum[ii], _animatedObject[ii].anim_object, _animatedObject[ii].defaultScale, _animatedObject[ii].velX, _animatedObject[ii].velY, _animatedObject[ii].intensity);
			}
		}
	}

	private void Play()
	{
		_audioSource.Stop();
		_audioSource.Play();
	}

	private void Play(float start_min, float start_max)
	{
		_audioSource.Stop();
		_audioSource.Play();
		_audioSource.time = Random.Range(start_min, start_max);
	}

	private void OnRetryButtonPress()
	{
		Play(0, _audioSource.clip.length / 4.0f);
	}


	public void UpdateAudioSetting()
	{
		_audioSource.volume = _volume.value;
	}
}

public class AnimatedObject: MonoBehaviour
{
	public GameObject anim_object;
	public Vector3 defaultScale;
	public float velX;
	public float velY;
	public float intensity;

	public AnimatedObject(GameObject _object, Vector3 _scale)
	{
		this.anim_object = _object;
		this.defaultScale = _scale;
		intensity = 1.0f;
	}
	public AnimatedObject(GameObject _object, Vector3 _scale, float _intensity)
	{
		this.anim_object = _object;
		this.defaultScale = _scale;
		this.intensity = _intensity;
	}

}
