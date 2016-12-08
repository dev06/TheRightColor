using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UI;
public class DialogHandler : MonoBehaviour {

	private Image _overlay;
	private float _overlayAlpha;
	private Color _overlayColor;

	private Color _containerColor;
	private Text _positiveButton;
	private Text _negativeButton;
	private Image _containerOverlay;
	private CanvasGroup _containerGroup;


	public Color _overlayTargetColor;
	public Color _containerTargetColor;

	void OnEnable()
	{
		EventManager.OnDialogPositive += OnDialogPositive;
		EventManager.OnDialogNegative += OnDialogNegative;
		EventManager.OnSubelementPress += OnSubelementPress;
	}

	void OnDisable()
	{
		EventManager.OnDialogPositive -= OnDialogPositive;
		EventManager.OnDialogNegative -= OnDialogNegative;
		EventManager.OnSubelementPress -= OnSubelementPress;
	}

	void Start ()
	{
		_overlay = GetComponent<Image>();
		_containerOverlay = transform.FindChild("container").GetComponent<Image>();
		_containerGroup = transform.FindChild("container").GetComponent<CanvasGroup>();
		_positiveButton = transform.FindChild("container").FindChild("positive").GetComponent<Text>();
		_negativeButton = transform.FindChild("container").FindChild("negative").GetComponent<Text>();
		// _positiveButton.raycastTarget = false;
		// _negativeButton.raycastTarget = false;


	}


	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{

		}
	}

	private void SetOpen()
	{
		_overlay.color = _overlayTargetColor;
		_containerOverlay.color = _containerTargetColor;
		_containerGroup.alpha = 1;
	}

	private void SetClose()
	{

		_overlayColor.r = 0.0f;
		_overlayColor.g = 0.0f;
		_overlayColor.b = 0.0f;
		_overlayColor.a = 0.0f;


		_containerGroup.alpha = 0;

		_overlay.color = _overlayColor;
		_containerOverlay.color = new Color(0, 0, 0, 0);
	}


	private IEnumerator Open(float _duration)
	{

		while (Mathf.Abs(1.0f - _overlay.color.a) > .1f)
		{
			_overlayAlpha += Time.deltaTime * _duration;
			_overlayColor.r = _overlayTargetColor.r;
			_overlayColor.g = _overlayTargetColor.g;
			_overlayColor.b = _overlayTargetColor.b;
			_overlayColor.a = _overlayAlpha;


			_containerColor.r = _containerTargetColor.r;
			_containerColor.g = _containerTargetColor.g;
			_containerColor.b = _containerTargetColor.b;

			_containerColor.a = _overlayAlpha;

			_overlay.color = _overlayColor;
			_containerOverlay.color = _containerColor;
			_containerGroup.alpha = _overlayAlpha;
			yield return new WaitForSeconds(Time.deltaTime);
		}


		_overlayAlpha = _overlayTargetColor.a;

		_overlay.color = _overlayTargetColor;
		_containerOverlay.color = _containerColor;
		_containerGroup.alpha = _overlayAlpha;

	}

	private IEnumerator Close(float _duration)
	{

		while (Mathf.Abs(0.0f - _overlay.color.a) > .1f)
		{
			_overlayAlpha -= Time.deltaTime * _duration;

			_overlayColor.r = 0.0f;
			_overlayColor.g = 0.0f;
			_overlayColor.b = 0.0f;

			_overlayColor.a = _overlayAlpha;

			_containerColor.a = _overlayAlpha;

			_overlay.color = _overlayColor;
			_containerOverlay.color = _containerColor;
			_containerGroup.alpha = _overlayAlpha;
			yield return new WaitForSeconds(Time.deltaTime);
		}


		_overlayAlpha = 0.0f;

		_overlayColor.r = 0.0f;
		_overlayColor.g = 0.0f;
		_overlayColor.b = 0.0f;

		_overlay.color = _overlayColor;

		_containerOverlay.color = _containerColor;
		_containerGroup.alpha = _overlayAlpha;
	}

	void OnSubelementPress(ShopButton.Shop_ButtonID id)
	{
		StopAllCoroutines();
		//StartCoroutine("Open", 4.0f);
		SetOpen();
	}

	void OnDialogPositive()
	{
		StopAllCoroutines();
		SetClose();
		//StartCoroutine("Close", 4.0f);
	}
	void OnDialogNegative()
	{
		StopAllCoroutines();
		SetClose();
		//StartCoroutine("Close", 4.0f);
	}

}
