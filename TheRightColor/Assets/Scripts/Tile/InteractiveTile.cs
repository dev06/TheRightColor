using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveTile: Tile
{

	public GeneratorTile generatorTile;


	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
	}

	public override void SetColor(Color c)
	{

		tileImage.color = c;
	}

	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
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

		tileAnimation.Play(tileAnimation.clip.name);

	}
}
