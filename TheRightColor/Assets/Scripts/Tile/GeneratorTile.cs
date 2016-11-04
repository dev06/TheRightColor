using UnityEngine;
using UnityEngine.EventSystems;

public class GeneratorTile: Tile
{

	public Color color;

	void OnEnable()
	{
		EventManager.OnCorrectColor += OnCorrectColor;
	}

	void OnDisable()
	{
		EventManager.OnCorrectColor -= OnCorrectColor;
	}


	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);
		Debug.Log("Cikc");
	}

	void OnCorrectColor()
	{
		int _pick = Random.Range(0, 2);
		if (_pick == 0)
		{
			tileAnimation.Play("generator_anim_right");
		} else {
			tileAnimation.Play("generator_anim_left");
		}
	}
}
