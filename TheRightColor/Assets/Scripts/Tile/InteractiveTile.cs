using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveTile: Tile
{

	private bool _canPlayIncorrectAnim;
	private GameObject _overlay;

	public GeneratorTile generatorTile;

	public Animation interactiveTileAnim;


	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
		EventManager.OnIncorrectColor += OnIncorrectColor;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
		EventManager.OnIncorrectColor -= OnIncorrectColor;

	}

	void Start()
	{
		interactiveTileAnim = GameObject.FindWithTag("Container/InteractiveContainer").GetComponent<Animation>();
		_overlay = transform.FindChild("overlay").gameObject;
	}

	void Update()
	{
		if (GameManager.Instance.state == State.Game)
		{
			if (interactiveTileAnim.IsPlaying("interactive_tile_rotate_anim") == false && interactiveTileAnim.IsPlaying("interactive_tile_rotateright_anim") == false)
			{
				_canPlayIncorrectAnim = true;
			} else {
				_canPlayIncorrectAnim = false;
			}


			_overlay.SetActive((generatorTile.color == tileImage.color) && GameManager.Instance.score <= MasterVar.Tutorial_TileCount);

		}

	}
	public override void SetColor(Color c)
	{
		tileImage.color = c;
	}

	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
		if (GameManager.Instance.score == 0)
		{
			if (EventManager.OnFirstTouch != null)
			{
				EventManager.OnFirstTouch();
			}
		}

		if (tileImage.color == generatorTile.color)
		{
			if (EventManager.OnCorrectColor != null)
			{
				EventManager.OnCorrectColor();
			}

		} else {


			if (EventManager.OnIncorrectColor != null)
			{
				EventManager.OnIncorrectColor();
			}

		}
	}

	void OnCorrectColor()
	{
		int _pick = Random.Range(0, 2);
		if (_pick == 0)
		{
			interactiveTileAnim.Play("interactive_tile_rotate_anim");
		} else {
			interactiveTileAnim.Play("interactive_tile_rotateright_anim");
		}
	}

	void OnIncorrectColor()
	{
		if (_canPlayIncorrectAnim)
		{
			interactiveTileAnim.Play("interactive_tile_incorrect_anim");
		}
	}
}
