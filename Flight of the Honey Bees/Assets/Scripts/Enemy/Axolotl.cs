using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axolotl : Enemy {

	[SerializeField]
	float jumpForce;

	[SerializeField]
	float cooldown = 5f; // Jump cooldown

	[SerializeField]
	float detectionRange = 20f; // Range until it starts following

	float curCooldown = 0;
	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	void FixedUpdate() {
		if (!isActive) {
			return;
		}
		if (4 == BeeManager.beeManager.numBees) {
			return;
		} // Killer bee
		GameObject bee = BeeManager.beeManager.bees [BeeManager.beeManager.curBee];
		float distance = (bee.transform.position - this.transform.position).magnitude;
		if (distance < detectionRange) {
			curCooldown -= Time.fixedDeltaTime;
			if (curCooldown < 0) {
				curCooldown = cooldown;
				Jump ();

			}
		}

	}

	void Jump() {
		GameObject bee = BeeManager.beeManager.bees [BeeManager.beeManager.curBee];
		Vector2 jumpDirection = (bee.transform.position - this.transform.position).normalized;
		rb.AddForce (jumpDirection * jumpForce);
	}

}
