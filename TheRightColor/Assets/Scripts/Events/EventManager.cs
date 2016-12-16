using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void InteractiveTilePress();
	public static InteractiveTilePress OnCorrectColor;
	public static InteractiveTilePress OnIncorrectColor;


	public delegate void StateChange();
	public static StateChange OnMenuStateActive;
	public static StateChange OnGameStateActive;
	public static StateChange OnSettingStateActive;
	public static StateChange OnGameOverStateActive;
	public static StateChange OnControlStateActive;
	public static StateChange OnCreditStateActive;
	public static ButtonPress OnPauseStateActive;


	public delegate void ButtonPress();
	public static ButtonPress OnPlayButtonPress;
	public static ButtonPress OnSettingButtonPress;
	public static ButtonPress OnCreditButtonPress;
	public static ButtonPress OnControlButtonPress;
	public static ButtonPress OnRetryButtonPress;
	public static ButtonPress OnPauseButtonPress;


	public delegate void ShopButtonPress(ShopButton.Shop_ButtonID id);
	public static ShopButtonPress OnSubelementPress;



	public delegate void GamePlay();
	public static GamePlay OnFirstTouch;
	public static GamePlay OnBreakStreak;


	public delegate void Timer();
	public static Timer OnTimerUp;

	public delegate void MenuIntroAnimation();
	public static MenuIntroAnimation OnButtonTouch;

	public delegate void MenuSwipeAnimation();
	public static MenuSwipeAnimation OnSwipeAninmationFinished;


	public delegate void Swipe();
	public static Swipe OnSwipeLeft;
	public static Swipe OnSwipeRight;
	public static Swipe OnSwipeUp;
	public static Swipe OnSwipeDown;

	public delegate void Dialog();
	public static Dialog OnDialogPositive;
	public static Dialog OnDialogNegative;
	public static Dialog OnDialogConfirm;



	public delegate void Game();
	public static Game ResetGame;

}
