using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour {
	[SerializeField]
	GameObject[] enemies;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			foreach (GameObject enemy in enemies) {
				enemy.GetComponent<Enemy> ().isActive = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			foreach (GameObject enemy in enemies) {
				enemy.GetComponent<Enemy> ().isActive = false;
			}
		}
	}
}
