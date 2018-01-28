using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	Slider slider;
	float endOfLevel;
	GameObject camera;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		endOfLevel = camera.GetComponent<PlayerCamera> ().levelEnd;
		slider.maxValue = endOfLevel;
		slider.minValue = 0;
		slider.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = camera.transform.position.x;
	}
}
