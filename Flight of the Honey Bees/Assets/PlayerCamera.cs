using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	[SerializeField]
	public float levelEnd = 97;

	[SerializeField]
	float cameraSpeed = .05f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (gameObject.transform.position.x < levelEnd) {
			gameObject.transform.position += Vector3.right * cameraSpeed;
		}
	}
}
