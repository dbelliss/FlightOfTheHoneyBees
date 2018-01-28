using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
	[SerializeField]
	float damage; // Amount of damage water does

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			BeeManager.TakeDamage (damage);
		}
	}
}
