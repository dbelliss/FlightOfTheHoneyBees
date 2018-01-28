using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour {
	[SerializeField]
	public float levelEnd = 120;

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
		else {
			StartCoroutine (WaitForWin ());
		}
	}

	IEnumerator WaitForWin() {
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene ("Win");
	}
}
