using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasonBee : Bee {
	[SerializeField]
	GameObject projectile; // Project to shoot
	[SerializeField]
	Vector3 spawnDistance = new Vector3 (.5f, -.5f,0);

	[SerializeField]
	float cooldown = 1f;
	float curCooldown = 1f;

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	new void FixedUpdate() {
		base.FixedUpdate ();
		curCooldown -= Time.fixedDeltaTime;
		if (BeeManager.curBee == beeNumber) {
			Shoot ();
		}
	}

	void Shoot() {
		if (Input.GetAxisRaw("Jump") == 1 && curCooldown < 0) {
			curCooldown = cooldown;
			Instantiate (projectile, this.transform.position + spawnDistance, Quaternion.identity);
		}
	}

}
