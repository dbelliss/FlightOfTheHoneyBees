using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badger : Enemy {
	[SerializeField]
	float speed;
	[SerializeField]
	float range;
	Vector2 startingPos;
	// Use this for initialization
	void Start () {
		base.Start ();
		startingPos = transform.position;
		rb.velocity = Vector2.right * speed;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void FixedUpdate() {
		if (transform.position.x > startingPos.x + range) {
			//start moving left
			rb.velocity = Vector2.left * speed;
		}
		else if (transform.position.x < startingPos.x) {
			// Start moving right
			rb.velocity = Vector2.right * speed;
		}
		else {
			// Continue with current velocity
		}
	}
}
