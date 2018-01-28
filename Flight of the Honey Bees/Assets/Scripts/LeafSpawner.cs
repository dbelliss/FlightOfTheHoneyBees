using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour {
	public GameObject leaf;
	[SerializeField]
	float spawnIntervals = 1f;
	BoxCollider2D bc;
	// Use this for initialization
	void Start () {
		bc = GetComponent<BoxCollider2D> (); // Use collider to spawn from
		StartCoroutine(SpawnLeaf());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		
	}


	IEnumerator SpawnLeaf() {
		while (true) {
			float spawnPositionX = Random.Range (-1f, 1f);
			Vector2 spawnPosition = new Vector2 (this.transform.position.x + spawnPositionX * bc.bounds.size.x/2, this.transform.position.y);
			Instantiate (leaf, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(spawnIntervals);
		}
	}
}
