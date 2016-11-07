using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	public static GameManager Instance;


	public float score;
	public float highscore;
	public State state;

	void Awake () {
		if (Instance != null)
		{
			Destroy(this);
		} else {
			Instance = this;
		}
		state = State.Control;

	}

	void Start()
	{

	}


	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
	}


	// Update is called once per frame
	void Update () {

	}


	void OnCorrectColor()
	{
		score++;
	}
}



