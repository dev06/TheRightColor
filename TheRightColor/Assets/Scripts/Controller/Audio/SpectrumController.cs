using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class SpectrumController : MonoBehaviour {


	private delegate void Volume(bool active);
	private static Volume OnVolumeActive;


	private AudioSource _audioSource;
	private int _barCount;
	private float _pitchVel;
	private float vel;
	private float _pauseVel;
	private float[] spectrum = new float[64];
	private Transform _barContainer;
	private RectTransform _barContainerRT;
	private List<AnimatedObject> _animatedObject;
	private Slider _volume;
	private bool _inPause;

	private List<AnimatedObject> _menuObjects;
	private List<AnimatedObject> _gameObjects;
	private List<AnimatedObject> _tutorialObjects;
	private List<AnimatedObject> _creditObjects;
	private List<AnimatedObject> _settingObjects;




	void OnEnable()
	{
		EventManager.OnGameStateActive += OnGameStateActive;
		EventManager.OnPauseStateActive += OnPauseStateActive;
		EventManager.OnRetryButtonPress += OnRetryButtonPress;
		EventManager.OnTimerUp += OnTimerUp;
		OnVolumeActive += OnVolumePause;

	}


	void OnDisable()
	{
		EventManager.OnGameStateActive -= OnGameStateActive;
		EventManager.OnPauseStateActive -= OnPauseStateActive;
		EventManager.OnRetryButtonPress -= OnRetryButtonPress;
		EventManager.OnTimerUp -= OnTimerUp;
		OnVolumeActive -= OnVolumePause;
	}

	void Start ()
	{
		_barCount = 5;
		_barContainer = transform.GetChild(0);
		_barContainerRT = _barContainer.GetComponent<RectTransform>();

		_audioSource = GetComponent<AudioSource>();
		_animatedObject = new List<AnimatedObject>();

		_menuObjects = new List<AnimatedObject>();
		_gameObjects = new List<AnimatedObject>();
		_creditObjects = new List<AnimatedObject>();
		_settingObjects = new List<AnimatedObject>();
		_tutorialObjects = new List<AnimatedObject>();

		GameObject _interactiveContainer = GameObject.FindWithTag("Container/InteractiveContainer");
		GameObject _generatorTile = GameObject.FindWithTag("Tile/GeneratorTile");
		GameObject _buttonContainer = GameObject.FindWithTag("Container/ButtonContainer");
		GameObject _score = GameObject.FindWithTag("Board/Score");
		GameObject _shopContainer = GameObject.FindWithTag("Container/ShopContainer");
		GameObject _creditContainer = GameObject.FindWithTag("Container/CreditContainer");
		GameObject _settingContainer = GameObject.FindWithTag("Container/SettingContainer");
		GameObject _challengeContainer = GameObject.FindWithTag("Container/SettingContainer");
		GameObject _effects = GameObject.FindWithTag("Effects");



		_gameObjects.Add(new AnimatedObject(_interactiveContainer, _interactiveContainer.transform.localScale));
		_gameObjects.Add(new AnimatedObject(_generatorTile, _generatorTile.transform.localScale));
		_gameObjects.Add(new AnimatedObject(_effects, _effects.transform.localScale, 2.0f));
		_gameObjects.Add(new AnimatedObject(_score, _score.transform.localScale, 10.0f));
		_gameObjects.Add(new AnimatedObject(_challengeContainer, _challengeContainer.transform.localScale, 3.0f));

		_menuObjects.Add(new AnimatedObject(_buttonContainer, _buttonContainer.transform.localScale));
		_tutorialObjects.Add(new AnimatedObject(_shopContainer, _shopContainer.transform.localScale, 0.3f));
		_creditObjects.Add(new AnimatedObject(_creditContainer, _creditContainer.transform.localScale));
		_settingObjects.Add(new AnimatedObject(_settingContainer, _settingContainer.transform.localScale));

		_volume = GameObject.FindWithTag("SettingOption/Volume").GetComponent<Slider>();
		PopulateBars();
		UpdateAudioSetting();
		//_audioSource.time = Random.Range(0, _audioSource.clip.length / 4.0f);

		if (PlayerPrefs.HasKey("Volume"))
		{

			_volume.value = PlayerPrefs.GetFloat("Volume");

		}
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
		//_audioSource.UnPause();
		_inPause = false;
	}

	void OnPauseStateActive()
	{
		//_audioSource.Pause();
		_inPause = true;
	}

	void OnVolumePause(bool active)
	{
		if (active)
		{
			_audioSource.Pause();
		} else {
			_audioSource.UnPause();
		}
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
				_scaleY = Mathf.SmoothDamp(_scaleY, spectrum[i] * 20, ref vel, .2f);
				if (_scaleY < 0) { _scaleY = 0; }
				_rectTransform.localScale = new Vector3(1, _scaleY, 1);

			}

			AnimateContainers(GameManager.Instance.state);

		}

		if (GameManager.Instance.state == State.Pause)
		{
			if (_audioSource.volume <= .1f)
			{
				if (OnVolumeActive != null)
				{
					OnVolumeActive(true);
				}
			} else {

				DampVolume(0);
			}
		} else {
			if (OnVolumeActive != null)
			{
				OnVolumeActive(false);
			}
			DampVolume(_volume.value);
		}


	}

	private void AnimateContainers(State state)
	{
		switch (state)
		{
			case State.Menu:
			{
				for (int ii = 0; ii < _menuObjects.Count; ii++)
				{
					UpdateTileEntities(spectrum[ii], _menuObjects[ii].anim_object, _menuObjects[ii].defaultScale, _menuObjects[ii].velX, _menuObjects[ii].velY, _menuObjects[ii].intensity);
				}
				break;
			}

			case State.Game:
			{
				for (int ii = 0; ii < _gameObjects.Count; ii++)
				{
					UpdateTileEntities(spectrum[ii], _gameObjects[ii].anim_object, _gameObjects[ii].defaultScale, _gameObjects[ii].velX, _gameObjects[ii].velY, _gameObjects[ii].intensity);
				}
				break;

			}

			case State.Control:
			{
				for (int ii = 0; ii < _tutorialObjects.Count; ii++)
				{
					UpdateTileEntities(spectrum[ii], _tutorialObjects[ii].anim_object, _tutorialObjects[ii].defaultScale, _tutorialObjects[ii].velX, _tutorialObjects[ii].velY, _tutorialObjects[ii].intensity);
				}
				break;
			}

			case State.Credit:
			{
				for (int ii = 0; ii < _creditObjects.Count; ii++)
				{
					UpdateTileEntities(spectrum[ii], _creditObjects[ii].anim_object, _creditObjects[ii].defaultScale, _creditObjects[ii].velX, _creditObjects[ii].velY, _creditObjects[ii].intensity);
				}
				break;

			}
			case State.Setting:
			{
				for (int ii = 0; ii < _settingObjects.Count; ii++)
				{
					UpdateTileEntities(spectrum[ii], _settingObjects[ii].anim_object, _settingObjects[ii].defaultScale, _settingObjects[ii].velX, _settingObjects[ii].velY, _settingObjects[ii].intensity);
				}
				break;

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

	/// <summary>
	/// Triggered when Retry button is pressed from pause
	/// </summary>
	private void OnRetryButtonPress()
	{
		Play(0, _audioSource.clip.length / 4.0f);
	}


	private void UpdateAudioSetting()
	{
		_audioSource.volume = _volume.value;
	}

	/// <summary>
	/// Damps the volumes
	/// </summary>
	/// <param name="volume"></param>
	private void DampVolume(float volume)
	{
		_audioSource.volume = Mathf.SmoothDamp(_audioSource.volume, volume, ref _pauseVel, .2f);
	}

	/// <summary>
	/// Puts the current volume in playerprefs
	/// </summary>
	private void OnTimerUp()
	{
		PlayerPrefs.SetFloat("Volume", _volume.value);
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
