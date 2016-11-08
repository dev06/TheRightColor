using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ParticleController : MonoBehaviour {

	// Use this for initialization
	Vector3 pos;
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{


		// if (Input.GetMouseButtonDown(0))
		// {
		// 	Debug.Log(Input.mousePosition);
		// 	Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// 	pz.z = 0;
		// 	Instantiate(AppResources.Touch_PS, pz, transform.rotation);

		// // }
		// int nbTouches = Input.touchCount;

		// if (nbTouches > 0)
		// {
		// 	print(nbTouches + " touch(es) detected");

		// 	for (int i = 0; i < nbTouches; i++)
		// 	{
		// 		Touch touch = Input.GetTouch(i);

		// 		print("Touch index " + touch.fingerId + " detected at position " + touch.position);
		// 		Instantiate(AppResources.Touch_PS, new Vector3(touch.position.x, touch.position.y, 0), Quaternion.identity);
		// 		GameObject.FindWithTag("text").GetComponent<Text>().text = touch.position + "asf";
		// 	}
		// }

		//if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
		// if (Input.GetMouseButtonDown(0))
		// {
		// 	Vector3 fingerPos = Input.mousePosition;
		// 	fingerPos.z = 0;
		// 	Debug.Log(Input.mousePosition);
		// 	Vector3 objPos = Camera.main.ScreenToWorldPoint (fingerPos);

		// 	Instantiate (AppResources.Touch_PS, objPos, Quaternion.identity) ;

		// }

		//}


	}
}
