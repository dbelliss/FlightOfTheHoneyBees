using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hornet : Enemy {
	public float speed = 1;
	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void FixedUpdate() {
		if (BeeManager.beeManager.curBee > 3  || !isActive) {
			return;
		}
		GameObject player = BeeManager.beeManager.bees [BeeManager.beeManager.curBee];
		Vector2 toPlayer = (player.transform.position - this.gameObject.transform.position).normalized;
		rb.velocity = toPlayer * speed;
	}
}
