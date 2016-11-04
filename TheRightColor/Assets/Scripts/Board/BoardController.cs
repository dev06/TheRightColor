using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
/// <summary>
/// Used for managing board
/// </summary>
public class BoardController : MonoBehaviour {

	public static int BoardSize = 2;
	public GameObject generatorTile;
	public List<InteractiveTile> interactiveTile;

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
		GenerateGenerator();
		GenerateBoard(BoardSize, BoardSize);
	}

	void Start()
	{

	}


	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			GenerateInteractiveTileColor();
		}
	}

	/// <summary>
	/// Generates Board
	/// </summary>
	/// <param name="xCount"></param>
	/// <param name="yCount"></param>
	private void GenerateBoard(int xCount, int yCount)
	{
		interactiveTile = new List<InteractiveTile>();
		for (int y = 0; y < yCount; y++)
		{
			for (int x = 0; x < xCount; x++)
			{
				float _tileSpacing = .9f;
				float _edgeSpacing = 1.02f;
				GameObject _tile = (GameObject)Instantiate((GameObject)AppResources.InteractiveTile, Vector3.zero, Quaternion.identity);
				GameObject _tileContainer = GameObject.FindWithTag("Container/InteractiveContainer");
				RectTransform _rectTransform = _tile.GetComponent<RectTransform>();
				RectTransform _tileContainerTransform = _tileContainer.GetComponent<RectTransform>();
				_tile.transform.SetParent(_tileContainer.transform);
				_rectTransform.localScale = new Vector3(1, 1, 1);
				_rectTransform.sizeDelta = new Vector3((_tileContainerTransform.sizeDelta.x / xCount) * _tileSpacing, (_tileContainerTransform.sizeDelta.y / yCount) * _tileSpacing);
				_tile.GetComponent<Image>().sprite = SetCornerTile(x, y, xCount, yCount,  _tile.GetComponent<Image>().sprite);
				float _xPos = x * _rectTransform.sizeDelta.x * _edgeSpacing;
				float _yPos = y * _rectTransform.sizeDelta.y * _edgeSpacing;
				float _spacingDiff_x = _rectTransform.sizeDelta.x * _edgeSpacing - _rectTransform.sizeDelta.x;
				float _spacingDiff_y = _rectTransform.sizeDelta.y * _edgeSpacing - _rectTransform.sizeDelta.y;
				float _xOffset = _tileContainerTransform.sizeDelta.x / 2 -  ((_rectTransform.sizeDelta.x + _spacingDiff_x / (xCount / 2.0f))  * (xCount / 2.0f));
				float _yOffset = _tileContainerTransform.sizeDelta.y / 2 -  ((_rectTransform.sizeDelta.y + _spacingDiff_y / (yCount / 2.0f))  * (yCount / 2.0f));
				_rectTransform.localPosition = new Vector3(_xPos + _xOffset, _yPos + _yOffset);
				interactiveTile.Add(_tile.GetComponent<InteractiveTile>());
				_tile.GetComponent<InteractiveTile>().Init();
				_tile.GetComponent<InteractiveTile>().generatorTile = generatorTile.GetComponent<GeneratorTile>();
			}
		}
		GenerateInteractiveTileColor();
	}

	/// <summary>
	/// Generates the generator
	/// </summary>
	private void GenerateGenerator()
	{
		GameObject _generator = (GameObject)Instantiate((GameObject)AppResources.GeneratorTile, Vector3.zero, Quaternion.identity);
		GameObject _generatorContainer = GameObject.FindWithTag("Container/GeneratorContainer");
		_generator.transform.SetParent(_generatorContainer.transform);
		RectTransform _rectTransformGenerator = _generator.GetComponent<RectTransform>();
		_rectTransformGenerator.localPosition = new Vector3(0, 0, 1);
		_rectTransformGenerator.localScale = new Vector3(1, 1, 1);
		_rectTransformGenerator.sizeDelta = _generatorContainer.GetComponent<RectTransform>().sizeDelta;
		generatorTile = _generator;
		GenerateGeneratorColor();

	}

	/// <summary>
	/// This generates generator color
	/// </summary>
	private void GenerateGeneratorColor()
	{
		Color _color = new Color();
		_color.r = Random.Range(0.5f, 1.0f);
		_color.g = Random.Range(0.5f, 1.0f);
		_color.b = Random.Range(0.5f, 1.0f);
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

				float _difference = 0;

				_mixColor.r = Random.Range(0.5f, 1.0f);
				_mixColor.g = Random.Range(0.5f, 1.0f);
				_mixColor.b = Random.Range(0.5f, 1.0f);
				_mixColor.a = 1.0f;
				_mixColor = NormalizeColor(_mixColor);
				_tile.SetColor(_mixColor);
			}
		}
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
		if (_normalizedColor.r > 1.0f) _normalizedColor.r = 1.0f;
		if (_normalizedColor.g > 1.0f) _normalizedColor.g = 1.0f;
		if (_normalizedColor.b > 1.0f) _normalizedColor.b = 1.0f;
		if (_normalizedColor.r < 0.0f) _normalizedColor.r = 0.0f;
		if (_normalizedColor.g < 0.0f) _normalizedColor.g = 0.0f;
		if (_normalizedColor.b < 0.0f) _normalizedColor.b = 0.0f;
		return _normalizedColor;
	}


	/// <summary>
	/// Event triggered when correct color is pressed
	/// </summary>
	private void OnCorrectColor()
	{
		GenerateGeneratorColor();
		GenerateInteractiveTileColor();
	}
}
