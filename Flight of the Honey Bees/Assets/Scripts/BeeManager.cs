using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour {
	public static int curBee = 0;
	public static GameObject[] bees = new GameObject[3];
	public static int numBees = 3;
	void Start () {
		
	}

	public static bool TakeDamage(float damage) {
		return bees [curBee].GetComponent<Bee> ().TakeDamage (damage);
	}
}
