using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour {
	[SerializeField]
	int beeNumber; // Bee number
	[SerializeField]
	string beeName = "Blank"; // Bee name
	[SerializeField]
	float speed = 1; // Speed of bee
	[SerializeField]
	float curHP = 2; // Health of bee
	[SerializeField]
	float maxHP = 2; // Health of bee

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
	public static Color nonMainColor = new Color (.2f, .2f, .2f,.7f);

	Rigidbody2D rb;
	BoxCollider2D bc;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		bc = GetComponent<BoxCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();
		BeeManager.bees [beeNumber] = this.gameObject;
		if (beeNumber != BeeManager.curBee) {
			DeactivateBee ();
		}
	}



	public void DeactivateBee() {
		bc.enabled = false; // Disable collision
		sr.color = nonMainColor; // Add grey tint
	}

	public void ActivateBee() {
		bc.enabled = true; // Enable collision
		sr.color = Color.white; // Reset color
	}

	// Update is called once per frame
	void Update () {
		BeeManager.ReadSwap(); // Read in fixed update because of how keys are detected frame by frame
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
//				Debug.Log ("Setting velocity to " + rb.velocity.ToString ());
			}
			// If far below pivot, change velocity
			else if (transform.position.y < pivot.y - idleMovementRange){
				rb.velocity = Vector2.up * speed * idleFactor;
//				Debug.Log ("Setting velocity to " + rb.velocity.ToString ());
			}
		}
	}



	void ReadInput() {
		rb.velocity = new Vector2 (Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed); // Move player based on input
	}




	public bool TakeDamage(float damage) {
		Debug.Log ("Bee " + beeNumber.ToString () + " took " + damage.ToString() + " damage");
		curHP -= damage;
		if (curHP < 0) {
			Debug.Log ("Bee " + beeNumber.ToString () + " is dead :C");
			return true;
		}
		return false;
	}

	public float GetCurHealth() {
		return curHP;
	}

	public float GetMaxHealth() {
		return maxHP;
	}

	public string GetName() {
		return beeName;
	}
}
