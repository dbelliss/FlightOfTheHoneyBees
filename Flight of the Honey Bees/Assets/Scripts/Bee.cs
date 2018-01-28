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
	static float beginIdleRange = 1f; // How close before idling
	static float breakIdleRange = 2f;
	static float idleMovementRange = .5f;
	public bool isIdling {
		get {
			return _isIdling;
		}
	}

	public Vector2 idlePosition {
		get {
			return _idlePosition;
		}
	}
		
	private bool _isIdling = false;
	private Vector2 _idlePosition;


	Rigidbody2D rb;
	BoxCollider2D bc;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		bc = GetComponent<BoxCollider2D> ();
		BeeManager.bees [beeNumber] = this.gameObject;
		if (beeNumber != BeeManager.curBee) {
			DeactivateBee ();
		}
	}



	void DeactivateBee() {
		bc.enabled = false;
		Debug.Log ("Disabling bee " + beeNumber.ToString ());
	}

	void ActivateBee() {
		bc.enabled = true;
		
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

		GameObject toFollow = BeeManager.bees [toFollowNum];
		Vector2 vectorDistance = toFollow.transform.position - gameObject.transform.position;
		float floatDistance = vectorDistance.magnitude;
		// If not in idle range, continue to get close to cur bee
		Bee followBee = toFollow.GetComponent<Bee> ();
		Debug.Log (floatDistance);
		// If idling, leader must be farther away before following
		if (isIdling) {
			// Only break from idle is distance is greater than idle break
			if (floatDistance > breakIdleRange) {
				// Move closer to cur bee
				_isIdling = false; // No longer idling
				rb.velocity = vectorDistance.normalized * speed;
			}
		}
		// If not idling, get within idle distance
		else if (floatDistance > beginIdleRange) {
			// Move closer to cur bee
			_isIdling = false; // No longer idling
			rb.velocity = vectorDistance.normalized * speed;
		}
		// Just idle
		else {
			float idleFactor = .5f;
			if (!_isIdling) {
				Debug.Log("Beginning to idle");
				_isIdling = true;
				rb.velocity = Vector2.down * speed; // Default to down
				_idlePosition = transform.position; // Save position before idling
			}

			//Move slightly up or down based on pivot
			Vector2 pivot = followBee.idlePosition - Vector2.right;



			// If above pivot, change velocity
			if (transform.position.y >= pivot.y + idleMovementRange) {
				// Move down

				rb.velocity = Vector2.down * speed * idleFactor;
				Debug.Log ("Setting velocity to " + rb.velocity.ToString ());
			}
			// If far below pivot, change velocity
			else if (transform.position.y < pivot.y - idleMovementRange){
				rb.velocity = Vector2.up * speed * idleFactor;
				Debug.Log ("Setting velocity to " + rb.velocity.ToString ());
			}
		}
	}



	void ReadInput() {
		rb.velocity = new Vector2 (Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed); // Move player based on input

	
	}



	bool WasKeyPressed(string keyString) {
		return Input.GetKey(keyString);
	}
}
