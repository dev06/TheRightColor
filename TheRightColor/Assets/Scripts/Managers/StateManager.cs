using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

	public static StateManager Instance;
	private Transform _canvasList;


	public State[,] stateArray;

	public int stateArrayIndex_x;
	public int stateArrayIndex_y;


	void OnEnable()
	{
		// EventManager.OnGameStateActive += OnGameStateActive;
		// EventManager.OnControlStateActive += OnControlStateActive;
		EventManager.OnSwipeAninmationFinished += OnSwipeAninmationFinished;

		EventManager.OnSwipeLeft += OnSwipeLeft;
		EventManager.OnSwipeRight += OnSwipeRight;
		EventManager.OnSwipeUp += OnSwipeUp;
		EventManager.OnSwipeDown += OnSwipeDown;

		EventManager.OnTimerUp += OnTimerUp;

	}

	void OnDisable()
	{
		// EventManager.OnGameStateActive -= OnGameStateActive;
		// EventManager.OnControlStateActive -= OnControlStateActive;
		EventManager.OnSwipeAninmationFinished -= OnSwipeAninmationFinished;

		EventManager.OnSwipeDown -= OnSwipeDown;
		EventManager.OnSwipeUp -= OnSwipeUp;
		EventManager.OnSwipeLeft -= OnSwipeLeft;
		EventManager.OnSwipeRight -= OnSwipeRight;

		EventManager.OnTimerUp -= OnTimerUp;

	}

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(this);
		} else
		{
			Instance = this;
		}

		stateArray = new State[3, 3];
		PopulateStateArray();
		stateArrayIndex_x = 1;
		stateArrayIndex_y = 1;

	}




	void PopulateStateArray()
	{
		for (int y = 0; y < 3; y++)
		{
			for (int x = 0; x < 3; x++)
			{
				if (x == 1 && y == 1)
				{
					stateArray[x, y] = State.Menu;
				}

				if (x == 0 && y == 1)
				{
					stateArray[x, y] = State.Control;
				}

				if (x == 1 && y == 2)
				{
					stateArray[x, y] = State.Credit;
				}

				if (x == 2 && y == 1)
				{
					stateArray[x, y] = State.Game;
				}
				if (x == 1 && y == 0)
				{
					stateArray[x, y] = State.Setting;
				}
			}
		}
	}

	void Start ()
	{
		_canvasList = GameObject.FindWithTag("Container/CanvasContainer").transform;
		SetState(stateArray[stateArrayIndex_x, stateArrayIndex_y]);

	}

	void Update ()
	{

	}


	void OnSwipeAninmationFinished()
	{
		SetState(stateArray[stateArrayIndex_x, stateArrayIndex_y]);

	}


	void OnSwipeRight()
	{
		if (stateArrayIndex_y == 1)
		{
			if (stateArrayIndex_x < 2) {
				stateArrayIndex_x++;
			}
		}

	}


	void OnSwipeLeft()
	{
		if (stateArrayIndex_y == 1)
		{
			if (stateArrayIndex_x > 0)
			{
				stateArrayIndex_x--;
			}
		}
	}


	void OnSwipeUp()
	{
		if (stateArrayIndex_y < 2) {
			stateArrayIndex_y++;
		}
	}


	void OnSwipeDown()
	{
		//SetState(State.Control);
		if (stateArrayIndex_y > 0)
		{
			stateArrayIndex_y--;
		}
	}




	void OnTimerUp()
	{
		if (stateArrayIndex_x > 0)
		{
			stateArrayIndex_x--;
		}
	}


	public void EnableCanvas(string _canvasTag)
	{
		for (int i = 0; i < _canvasList.childCount; i++)
		{
			//_canvasList.GetChild(i).GetComponent<Canvas>().enabled = true;
			if (_canvasList.GetChild(i).gameObject.tag.Contains(_canvasTag) == false)
			{
				//_canvasList.GetChild(i).GetComponent<Canvas>().enabled = false;
			} else {
			}
		}
	}

	public void SetState(State state)
	{
		switch (state)
		{
			case State.Game:
			{
				//EnableCanvas("GameCanvas");
				GameManager.Instance.state = state;
				break;
			}

			case State.Menu:
			{
				//	EnableCanvas("MenuCanvas");
				GameManager.Instance.state = state;
				break;
			}

			case State.Control:
			{
				//	EnableCanvas("ControlCanvas");
				GameManager.Instance.state = state;
				break;
			}
			case State.Setting:
			{
				GameManager.Instance.state = state;
				break;
			}
			case State.Credit:
			{
				GameManager.Instance.state = state;
				break;
			}
		}
	}
}

public enum State
{
	Game,
	Menu,
	Setting,
	Control,
	Credit,
	GameOver,
}
