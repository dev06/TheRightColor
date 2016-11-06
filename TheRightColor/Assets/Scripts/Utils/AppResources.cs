using UnityEngine;
using System.Collections;

public class AppResources
{

	public static Texture2D InteractiveTile_Texture = (Texture2D)Resources.Load("Textures/interactive_tile");
	public static Texture2D InteractiveTile_TopLeft_Texture = (Texture2D)Resources.Load("Textures/interactive_tile_topleft");
	public static Texture2D InteractiveTile_TopRight_Texture = (Texture2D)Resources.Load("Textures/interactive_tile_topright");
	public static Texture2D InteractiveTile_BottomLeft_Texture = (Texture2D)Resources.Load("Textures/interactive_tile_bottomleft");
	public static Texture2D InteractiveTile_BottomRight_Texture = (Texture2D)Resources.Load("Textures/interactive_tile_bottomright");



	public static GameObject InteractiveTile = (GameObject)Resources.Load("Prefabs/Tile/InteractiveTile");
	public static GameObject AudioSpecturmBar = (GameObject)Resources.Load("Prefabs/UI/bar");

	public static GameObject Touch_PS = (GameObject)Resources.Load("Prefabs/Particle/Touch");


	public static GameObject GeneratorTile = (GameObject)Resources.Load("Prefabs/Tile/GeneratorTile");

}
