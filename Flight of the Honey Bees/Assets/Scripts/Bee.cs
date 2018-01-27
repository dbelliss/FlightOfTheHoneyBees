using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour {
	[SerializeField]
	int beeNumber; // Bee number
	[SerializeField]
	string name = "Blank"; // Bee name
	[SerializeField]
	float speed = 1; // Speed of bee
	[SerializeField]
	int curHP = 1; // Health of bee
	[SerializeField]
	int maxHP = 1; // Health of bee

	[SerializeField]
	static float idleRange = 1f; // How close before idling

	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		BeeManager.bees [beeNumber] = this.gameObject;

	}

	// Update is called once per frame
	void Update () {
		ReadSwap(); // Read in fixed update because of how keys are detected frame by frame
	}

	void ReadSwap() {
		if (WasKeyPressed("1")) {
			BeeManager.curBee = 1;
		}
		else if (WasKeyPressed("2")) {
			BeeManager.curBee = 2;
		}
		else if (WasKeyPressed("3")) {
			BeeManager.curBee = 3;
		}
	}

	void FixedUpdate() {
		if (BeeManager.curBee == this.beeNumber) {
			ReadInput ();
		}
		else {
			FollowPlayer ();
		}
	}

	// Follow the main bee
	void FollowPlayer() {
		int toFollowNum = (1 + this.beeNumber) % BeeManager.numBees;
		Debug.Log (toFollowNum);
		GameObject toFollow = BeeManager.bees [toFollowNum];
		Vector2 vectorDistance = toFollow.transform.position - gameObject.transform.position;
		float floatDistance = vectorDistance.magnitude;
		// If not in idle range, continue to get close to cur bee
		if (floatDistance > idleRange) {
			// Move closer to cur bee
			rb.velocity = vectorDistance.normalized * speed;
		}
		// Else in range, just circle around
		else {
			rb.velocity = Vector2.zero;
		}
	}



	void ReadInput() {
		rb.velocity = new Vector2 (Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed); // Move player based on input

	
	}



	bool WasKeyPressed(string keyString) {
		return Input.GetKey(keyString);
	}
}
