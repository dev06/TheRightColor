using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	void Awake () {
		if (Instance != null)
		{
			Destroy(this);
		} else {
			Instance = this;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
