using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[SerializeField]
	float speed = 1;
	[SerializeField]
	float damage;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = Vector2.right * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Leaf") {
			Destroy (col.gameObject);
			Destroy (this.gameObject);
		}
		else if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<Enemy> ().TakeDamage (damage);
			Destroy (this.gameObject);
		}
	}
}
