﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour {

	private static float timeLastHit = 0.0f;
	private bool isInvicible = false;
	[SerializeField]
	private static float invincibilityTime = 2f;
	[SerializeField]
	protected int beeNumber; // Bee number
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
	public bool isInParty = true;
	protected Rigidbody2D rb;
	protected BoxCollider2D bc;
	SpriteRenderer sr;
	// Use this for initialization
	protected void Start () {
		rb = GetComponent<Rigidbody2D> ();
		bc = GetComponent<BoxCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();
		if (!isInParty) {
			bc.isTrigger = true;
			return;
		}
		BeeManager.beeManager.bees [beeNumber] = this.gameObject;
		if (beeNumber != BeeManager.beeManager.curBee) {
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
	protected void Update () {
		if (!isInParty) {
			return;
		}
	}

	protected void FixedUpdate() {
		if (!isInParty) {
			return;
		}
		if (BeeManager.beeManager.curBee == this.beeNumber) {
			ReadInput ();
		}
		else {
			FollowPlayer ();
		}
	}

	// Follow the main bee
	void FollowPlayer() {
		int toFollowNum = (1 + this.beeNumber) % BeeManager.beeManager.numBees;
		while (BeeManager.beeManager.bees[toFollowNum] == null) {
			toFollowNum = (1 + toFollowNum) % BeeManager.beeManager.numBees;
		}
		GameObject toFollow = BeeManager.beeManager.bees [toFollowNum];
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
		if (isInvicible) {
			return false; // Do not allow damage in invincibility frames
		}
		curHP -= damage;
		StartCoroutine (InvincibilityFrames ());
		if (curHP <= 0) {
			BeeManager.beeManager.DeadBee (beeNumber);
			Destroy (this.gameObject);
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

	public void SetBeeNum(int num) {
		beeNumber = num;
	}

	void OnTriggerEnter2D(Collider2D col) {
		Bee b = col.GetComponent<Bee> ();
		if (b) {
			if (b.name == "African Killer Bee") {
				return;
			}
		}
		if (beeName == "African Killer Bee") {
			if (col.gameObject.tag == "Player" && !isInParty) {
				BeeManager.beeManager.StartKillerBee (this.gameObject);
			}
		}
		else if (col.gameObject.tag == "Player" && isInParty) {
			BeeManager.beeManager.GatherBee (col.gameObject,this.gameObject);
		}
	}

	IEnumerator InvincibilityFrames() {
		isInvicible = true;
		timeLastHit = Time.time;
		while (Time.time < timeLastHit + invincibilityTime) {
			// Blink sprite while invincible
			sr.enabled = !sr.enabled;
			yield return new WaitForSeconds (.2f);	
		}
		isInvicible = false;
		sr.enabled = true; // Set to visible
	}
}
