using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	float curHealth = 4;

	[SerializeField]
	float damage = 1; // Damage done to player on contact

	protected SpriteRenderer sr;
	protected Rigidbody2D rb;
	protected Animator animator;

	public bool isActive = true;
	// Use this for initialization
	protected void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();

	}
	// Update is called once per frame
	protected void Update () {
		if (rb.velocity.x >= .1) {
			sr.flipX = true;
		}
		else {
			sr.flipX = false;
		}
	}

	public void TakeDamage(float damage) {
		curHealth -= damage;
		animator.SetInteger ("Health", (int)curHealth);
		if (curHealth < 0) {
			this.enabled = false; // Do not move if no more legs
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			BeeManager.TakeDamage (damage);
		}
	}
}
