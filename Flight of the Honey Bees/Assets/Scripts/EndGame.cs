using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey && Time.time - startTime > 3 || Input.GetKey("escape") ) {
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
