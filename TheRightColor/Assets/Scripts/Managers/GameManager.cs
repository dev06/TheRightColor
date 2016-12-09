using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public int tileCount;
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

public class Save
{
	public static void SetBool(string key, bool state)
	{
		PlayerPrefs.SetInt(key, state ? 1 : 0);
	}

	public static bool GetBool(string key)
	{
		int value = PlayerPrefs.GetInt(key);

		if (value == 1)
		{
			return true;
		}

		else
		{
			return false;
		}
	}

	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}
}



