using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
/// <summary>
/// Used for managing board
/// </summary>
public class BoardController : MonoBehaviour {

	public static int BoardSize = 2;
	public static Color[] TileColor = new Color[20];
	public GameObject generatorTile;
	public List<InteractiveTile> interactiveTile;
	private Animator _flashAnimator;
	private float _hue;
	private Color _hueColor = new Color();
	private RectTransform _interactiveTileContainer;
	private Vector3 _interactiveTileContainer_pos;
	private Image _boardImage;
	private Vector2 _boardOffsetMin = new Vector2(0f, 150f);
	private Vector2 _boardOffsetMax = Vector2.zero;

	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
	}

	void Awake ()
	{
		PopulateTileColor(.6f, .8f);
		GenerateGenerator();

	}

	void Start()
	{
		GenerateBoard(BoardSize, BoardSize);

		GenerateInteractiveTileColor();
		_boardImage = GetComponent<Image>();
		_flashAnimator = transform.FindChild("FlashImage").GetComponent<Animator>();
		_interactiveTileContainer = GameObject.FindWithTag("Container/InteractiveContainer").GetComponent<RectTransform>();
		// /CalcualteBoardBounds();
	}


	void Update ()
	{
		if (_interactiveTileContainer != null)
		{
			_interactiveTileContainer.offsetMin = _boardOffsetMin;
			_interactiveTileContainer.offsetMax = _boardOffsetMax;
		}

		ChangeBackGround();
	}

	private void PopulateTileColor(float min, float max)
	{
		for (int i = 0; i < TileColor.Length; i++)
		{
			Color c = new Color();
			c.r = Random.Range(min, max) + GetSignValue(Random.Range(.001f, .15f));
			c.g = Random.Range(min, max) + GetSignValue(Random.Range(.001f, .15f));
			c.b = Random.Range(min, max) + GetSignValue(Random.Range(.001f, .15f));
			c.a = 1.0f;
			TileColor[i] = c;
		}
	}

	private float GetSignValue(float _value)
	{
		return Random.Range(0, 2) == 0 ? _value : -_value;
	}


	/// <summary>
	/// Generates Board
	/// </summary>
	/// <param name="xCount"></param>
	/// <param name="yCount"></param>
	private void GenerateBoard(int xCount, int yCount)
	{
		float canvas_width = GameObject.FindWithTag("Canvas/GameCanvas").GetComponent<RectTransform>().rect.width;
		float canvas_height = GameObject.FindWithTag("Canvas/GameCanvas").GetComponent<RectTransform>().rect.height;

		GameObject _testContainer = GameObject.FindWithTag("Container/InteractiveContainer").gameObject;

		float container_width =  Mathf.Abs(_testContainer.GetComponent<RectTransform>().rect.width);
		float container_height =  Mathf.Abs(_testContainer.GetComponent<RectTransform>().rect.height);

		interactiveTile = new List<InteractiveTile>();

		if (container_width > container_height)
		{
			for (int y = 0; y < yCount; y++)
			{
				for (int x = 0; x < xCount; x++)
				{
					float _tileSpacing = .9f;
					float _edgeSpacing = 1.02f;

					GameObject _tile = (GameObject)Instantiate((GameObject)AppResources.InteractiveTile, Vector3.zero, Quaternion.identity);
					RectTransform tile_RT = _tile.GetComponent<RectTransform>();

					RectTransform testContainer_RT = _testContainer.GetComponent<RectTransform>();

					_tile.transform.SetParent(testContainer_RT.transform);
					tile_RT.localScale = new Vector3(1, 1, 1);
					tile_RT.sizeDelta = new Vector2((container_height / xCount)* _tileSpacing, (container_height / yCount) * _tileSpacing);


					float _xPos = (x * tile_RT.rect.width + tile_RT.rect.width / 2) - (tile_RT.sizeDelta.x ) * xCount / 2.0f;;
					float _yPos = (y * tile_RT.rect.height  + tile_RT.rect.height / 2) - (tile_RT.rect.height ) * yCount / 2.0f;




					Vector2 position = new Vector2(_xPos * _edgeSpacing, _yPos * _edgeSpacing);
					tile_RT.anchoredPosition = position;
					_tile.GetComponent<Image>().sprite = SetCornerTile(x, y, xCount, yCount,  _tile.GetComponent<Image>().sprite);




					//_rectTransform.sizeDelta = new Vector3((container_height / xCount) * _tileSpacing, (container_width / yCount) * _tileSpacing);
					//float _yPos = (y * _rectTransform.sizeDelta.y  + _rectTransform.sizeDelta.y / 2) - (_rectTransform.sizeDelta.y ) * yCount / 2.0f;
					//float _xPos = (x * tile_RT.sizeDelta.x  + tile_RT.sizeDelta.x / 2) - (tile_RT.sizeDelta.x ) * xCount / 2.0f;

					//_rectTransform.localPosition = new Vector3(_xPos * _edgeSpacing, _yPos * _edgeSpacing, 1);


					interactiveTile.Add(_tile.GetComponent<InteractiveTile>());
					_tile.GetComponent<InteractiveTile>().Init();
					_tile.GetComponent<InteractiveTile>().generatorTile = generatorTile.GetComponent<GeneratorTile>();
				}

			}
		} else if (container_height > container_width)
		{


			for (int y = 0; y < yCount; y++)
			{
				for (int x = 0; x < xCount; x++)
				{


					GameObject _tile = (GameObject)Instantiate((GameObject)AppResources.InteractiveTile, Vector3.zero, Quaternion.identity);
					RectTransform tile_RT = _tile.GetComponent<RectTransform>();

					RectTransform testContainer_RT = _testContainer.GetComponent<RectTransform>();

					_tile.transform.SetParent(testContainer_RT.transform);
					tile_RT.localScale = new Vector3(1, 1, 1);
					tile_RT.sizeDelta = new Vector2(container_width / xCount, container_width / yCount);


					float _xPos = (x * tile_RT.rect.width + tile_RT.rect.width / 2) - (tile_RT.sizeDelta.x ) * xCount / 2.0f;;
					float _yPos = (y * tile_RT.rect.height  + tile_RT.rect.height / 2) - (tile_RT.rect.height ) * yCount / 2.0f;



					Vector2 position = new Vector2(_xPos, _yPos);
					tile_RT.anchoredPosition = position;
					_tile.GetComponent<Image>().sprite = SetCornerTile(x, y, xCount, yCount,  _tile.GetComponent<Image>().sprite);


					float _tileSpacing = .9f;
					float _edgeSpacing = 1.02f;

					//_rectTransform.sizeDelta = new Vector3((container_height / xCount) * _tileSpacing, (container_width / yCount) * _tileSpacing);
					//float _yPos = (y * _rectTransform.sizeDelta.y  + _rectTransform.sizeDelta.y / 2) - (_rectTransform.sizeDelta.y ) * yCount / 2.0f;
					//float _xPos = (x * tile_RT.sizeDelta.x  + tile_RT.sizeDelta.x / 2) - (tile_RT.sizeDelta.x ) * xCount / 2.0f;

					//_rectTransform.localPosition = new Vector3(_xPos * _edgeSpacing, _yPos * _edgeSpacing, 1);


					interactiveTile.Add(_tile.GetComponent<InteractiveTile>());
					_tile.GetComponent<InteractiveTile>().Init();
					_tile.GetComponent<InteractiveTile>().generatorTile = generatorTile.GetComponent<GeneratorTile>();
				}

			}
		}



	}

	// private void CalcualteBoardBounds()
	// {


	// Debug.Log(container_width + " " + container_height);

	// //float container_width = canvas_width -
	// //Restricted by height
	// if (canvas_width > canvas_height)
	// {

	// 	if (container_width > container_height)
	// 	{
	// 		int xi = 0;
	// 		int yi = 0;

	// 		for (int ii = 0; ii < _testContainer.transform.childCount; ii++)
	// 		{
	// 			RectTransform child = _testContainer.transform.GetChild(ii).GetComponent<RectTransform>();
	// 			Vector2 size = new Vector2(container_height / BoardSize, container_height / BoardSize);
	// 			child.sizeDelta = size;

	// 			Vector2 position = Vector2.zero;
	// 			if (xi < BoardSize)
	// 			{

	// 				if (xi < (int)BoardSize / 2)
	// 				{
	// 					position = new Vector2(-size.x / BoardSize, -size.y / BoardSize);
	// 				}

	// 				xi++;
	// 			} else
	// 			{



	// 			}
	// 			child.anchoredPosition = position;

	// 		}




	// 	}






	/// <summary>
	/// Generates the generator
	/// </summary>
	private void GenerateGenerator()
	{
		GameObject _generator = (GameObject)Instantiate((GameObject)AppResources.GeneratorTile, Vector3.zero, Quaternion.identity);
		GameObject _generatorContainer = GameObject.FindWithTag("Container/GeneratorContainer");
		_generator.transform.SetParent(_generatorContainer.transform);




		RectTransform _rectTransformGenerator = _generator.GetComponent<RectTransform>();
		_rectTransformGenerator.anchoredPosition = new Vector3(0, 0, 1);
		_rectTransformGenerator.localScale = new Vector3(1, 1, 1);

		_rectTransformGenerator.sizeDelta = new Vector3(_generatorContainer.GetComponent<RectTransform>().rect.height, _generatorContainer.GetComponent<RectTransform>().rect.height, 1);
		generatorTile = _generator;
		_rectTransformGenerator.offsetMin = Vector2.zero;
		_rectTransformGenerator.offsetMax = Vector2.zero;
		GenerateGeneratorColor();


	}

	/// <summary>
	/// This generates generator color
	/// </summary>
	private void GenerateGeneratorColor()
	{
		Color _color = new Color();
		float _minBound = 0.6f;
		float _maxBound = 0.8f;
		_color.r = Random.Range(_minBound, _maxBound);
		_color.g = Random.Range(_minBound, _maxBound);
		_color.b = Random.Range(_minBound, _maxBound);
		_color.a = 1.0f;
		generatorTile.GetComponent<Image>().color = _color;
		generatorTile.GetComponent<GeneratorTile>().Init();
		generatorTile.GetComponent<GeneratorTile>().color = _color;
	}

	/// <summary>
	/// This generates the interactive tile color
	/// </summary>
	private void GenerateInteractiveTileColor()
	{
		int _pick = Random.Range(0, interactiveTile.Count);
		Color _generatorColor = generatorTile.GetComponent<Image>().color;
		for (int i = 0; i < interactiveTile.Count; i++)
		{
			InteractiveTile _tile = interactiveTile[i];

			if (i == _pick)
			{
				_tile.SetColor(_generatorColor);
			} else {
				Color _mixColor = new Color();

				float _min = .6f;
				float _max = .7f;
				float _divider = 10.0f;
				float _r = Random.Range(_min, _max) + GetColorValue((float) i / _divider);
				float _g = Random.Range(_min, _max) + GetColorValue((float) i / _divider);
				float _b = Random.Range(_min, _max) + GetColorValue((float) i / _divider);
				Color _genColor = TileColor[Random.Range(0, TileColor.Length - 1)];


				_mixColor.r = _genColor.r;
				_mixColor.g = _genColor.g;
				_mixColor.b = _genColor.b;
				_mixColor.a = 1.0f;
				_mixColor = NormalizeColor(_mixColor);
				_tile.SetColor(_mixColor);
			}
		}
	}



	private float GetColorValue(float _difference)
	{
		return (Random.Range(0, 2) == 0) ? _difference : -_difference;
	}

	/// <summary>
	/// Sets the corner tile sprite
	/// </summary>
	private Sprite SetCornerTile(int x, int y, int xCount, int yCount,  Sprite _sprite)
	{
		int _cornerTileID;
		if (isCornerTile(x, y , xCount, yCount, out _cornerTileID))
		{

			switch (_cornerTileID)
			{
				case 0:
				{
					_sprite = Sprite.Create(AppResources.InteractiveTile_BottomLeft_Texture,
					                        new Rect(0, 0, AppResources.InteractiveTile_BottomLeft_Texture.width,
					                                 AppResources.InteractiveTile_BottomLeft_Texture.height),
					                        new Vector2(.5f, .5f));

					break;
				}
				case 1:
				{
					_sprite = Sprite.Create(AppResources.InteractiveTile_TopLeft_Texture,
					                        new Rect(0, 0, AppResources.InteractiveTile_TopLeft_Texture.width,
					                                 AppResources.InteractiveTile_TopLeft_Texture.height),
					                        new Vector2(.5f, .5f));

					break;
				}
				case 2:
				{
					_sprite = Sprite.Create(AppResources.InteractiveTile_TopRight_Texture,
					                        new Rect(0, 0, AppResources.InteractiveTile_TopRight_Texture.width,
					                                 AppResources.InteractiveTile_TopRight_Texture.height),
					                        new Vector2(.5f, .5f));

					break;
				}
				case 3:
				{
					_sprite = Sprite.Create(AppResources.InteractiveTile_BottomRight_Texture,
					                        new Rect(0, 0, AppResources.InteractiveTile_BottomRight_Texture.width,
					                                 AppResources.InteractiveTile_BottomRight_Texture.height),
					                        new Vector2(.5f, .5f));

					break;
				}
			}
		}

		return _sprite;
	}

	private bool isCornerTile(int x, int y, int xCount, int yCount, out int _cornerTileID)
	{
		_cornerTileID = -1;
		if (x == 0 && y == 0)
		{
			_cornerTileID = 0;
			return true;
		}

		if (x == 0 && y == yCount - 1)
		{
			_cornerTileID = 1;
			return true;
		}

		if (x == xCount - 1 && y == 0)
		{
			_cornerTileID = 3;
			return true;
		}

		if (x == xCount - 1 && y == yCount - 1)
		{
			_cornerTileID = 2;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Returns NoramlizedColor
	/// </summary>
	private Color NormalizeColor(Color _color)
	{
		Color _normalizedColor = _color;
		if (_normalizedColor.r > 1.0f) { _normalizedColor.r = 1.0f; }
		if (_normalizedColor.g > 1.0f) { _normalizedColor.g = 1.0f; }
		if (_normalizedColor.b > 1.0f) { _normalizedColor.b = 1.0f; }
		if (_normalizedColor.r < 0.0f) { _normalizedColor.r = 0.0f; }
		if (_normalizedColor.g < 0.0f) { _normalizedColor.g = 0.0f; }
		if (_normalizedColor.b < 0.0f) { _normalizedColor.b = 0.0f; }
		return _normalizedColor;
	}

	private AnimationClip GetAnimatorClipAtIndex(int index)
	{
		RuntimeAnimatorController ac = _flashAnimator.runtimeAnimatorController;
		AnimationClip[] clips = ac.animationClips;
		return clips[index];
	}


	private void ChangeBackGround()
	{
		_hue = Mathf.PingPong(Time.time / 15.0f, 1.0f);
		_hueColor = Color.HSVToRGB(_hue, .5f, .5f);
		_boardImage.color = _hueColor;
	}

	/// <summary>
	/// Event triggered when correct color is pressed
	/// </summary>
	private void OnCorrectColor()
	{
		GenerateGeneratorColor();
		GenerateInteractiveTileColor();

		_flashAnimator.SetTrigger("Trigger");
	}



}
