using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour {
	float damage = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			BeeManager.beeManager.TakeDamage (damage);
		}
	}

	public void TakeDamage() {
		GetComponent<PolygonCollider2D> ().enabled = false; // Do not damage player anymore
		GetComponent<Animator>().SetTrigger("Damage"); // Change to damaged animation
		StartCoroutine(Despawn()); // Despawn after a given time
	}

	IEnumerator Despawn() {
		yield return new WaitForSeconds(1f);
		Destroy(this.gameObject);
	}
}
