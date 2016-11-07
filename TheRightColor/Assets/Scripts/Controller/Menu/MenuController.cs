using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	public void GenerateBoard(int xCount, int yCount)
	{
		for (int y = 0; y < yCount; y++)
		{
			for (int x = 0; x < xCount; x++)
			{
				float _tileSpacing = .9f;
				float _edgeSpacing = 1.02f;
				GameObject _tile = (GameObject)Instantiate((GameObject)AppResources.InteractiveTile, Vector3.zero, Quaternion.identity);
				GameObject _tileContainer = GameObject.FindWithTag("Container/ButtonContainer");
				RectTransform _rectTransform = _tile.GetComponent<RectTransform>();
				RectTransform _tileContainerTransform = _tileContainer.GetComponent<RectTransform>();
				_tile.transform.SetParent(_tileContainer.transform);
				_rectTransform.localScale = new Vector3(1, 1, 1);
				_rectTransform.sizeDelta = new Vector3((_tileContainerTransform.sizeDelta.x / xCount) * _tileSpacing, (_tileContainerTransform.sizeDelta.y / yCount) * _tileSpacing);
				_tile.GetComponent<Image>().sprite = SetCornerTile(x, y, xCount, yCount,  _tile.GetComponent<Image>().sprite);

				float _xPos = (x * _rectTransform.sizeDelta.x  + _rectTransform.sizeDelta.x / 2) - (_rectTransform.sizeDelta.x ) * xCount / 2.0f;
				float _yPos = (y * _rectTransform.sizeDelta.y  + _rectTransform.sizeDelta.y / 2) - (_rectTransform.sizeDelta.y ) * yCount / 2.0f;
				_rectTransform.localPosition = new Vector3(_xPos * _edgeSpacing, _yPos * _edgeSpacing, 1);

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

}
