using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void InteractiveTilePress();
	public static InteractiveTilePress OnCorrectColor;
	public static InteractiveTilePress OnIncorrectColor;


}
