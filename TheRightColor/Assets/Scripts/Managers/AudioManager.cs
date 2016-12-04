using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private AudioSource _correct;

	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
	}
	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
	}
	void Start () {

		_correct = GetComponent<AudioSource>();
	}

	void Update () {

	}

	void OnCorrectColor()
	{
		//_correct.Play();
	}
}
