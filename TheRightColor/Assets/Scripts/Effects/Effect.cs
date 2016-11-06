using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	public float life;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Destroy(gameObject, life);
	}
}
