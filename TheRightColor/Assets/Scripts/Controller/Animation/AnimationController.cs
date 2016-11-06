using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	public static void Play(Animation _animation, string _name, int _direction)
	{
		if (_direction > 0)
		{
			_animation[_name].time = 0;
		} else {
			_animation[_name].time = _animation[_name].length;
		}
		_animation[_name].speed = _direction;
		_animation.Play(_name);
	}


}
