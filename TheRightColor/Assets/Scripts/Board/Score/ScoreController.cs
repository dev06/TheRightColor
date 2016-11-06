using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreController : MonoBehaviour {


	private Text _scoreText;

	private Animation _animation;

	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
		EventManager.OnTimerUp += OnTimerUp;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
		EventManager.OnTimerUp -= OnTimerUp;
	}

	void Start ()
	{
		_scoreText = transform.FindChild("scoreText").GetComponent<Text>();
		_animation = GetComponent<Animation>();

		if (PlayerPrefs.HasKey("Highscore"))
		{
			SetHighScore(PlayerPrefs.GetFloat("Highscore"));
		}
	}

	void Update ()
	{
		if (GameManager.Instance.score < 100)
		{
			_scoreText.text = GameManager.Instance.score + "";
		} else {
			_scoreText.text = GameManager.Instance.score + "!";
		}
	}

	void OnCorrectColor()
	{
		_animation.Play(_animation.clip.name);
	}

	void SetHighScore(float _score)
	{
		GameManager.Instance.highscore = _score;
		GameObject.FindWithTag("Stats/Highscore").GetComponent<Text>().text = _score.ToString();
		PlayerPrefs.SetFloat("Highscore", GameManager.Instance.highscore);
	}

	void OnTimerUp()
	{
		if (GameManager.Instance.score > GameManager.Instance.highscore)
		{
			SetHighScore(GameManager.Instance.score);
		}
	}
}
