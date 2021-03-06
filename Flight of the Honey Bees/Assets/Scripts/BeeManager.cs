﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeeManager : MonoBehaviour {
	public static BeeManager beeManager;
	public int curBee = 0;
	public List<GameObject> bees;
	public int numBees = 3;

	public Text[] beeNames;
	public Image[] beeImages;
	public Text[] beeHealths;

	public Sprite blankSprite;

	void Awake() {
		beeManager = this;
		bees = new List<GameObject> ();
		bees.Add (null);
		bees.Add (null);
		bees.Add (null);

		beeNames = new Text[numBees];
		beeImages = new Image[numBees];
		beeHealths = new Text[numBees];
	}

	// This start is guarenteed to happen after Bee.Start()
	void Start () {
		for (int i = 0; i < numBees; i++) {
			string beeNum = i.ToString ();
			beeNames [i] = GameObject.Find ("Bee" + beeNum + "Name").GetComponent<Text> ();
			beeImages [i] = GameObject.Find ("Bee" + beeNum + "Icon").GetComponent<Image> ();
			beeHealths [i] = GameObject.Find ("Bee" + beeNum + "HP").GetComponent<Text> ();
		}
		UpdateUI (); // Get correct health values
	}

	public bool TakeDamage(float damage) {
		if (curBee > 3) {
			return false;
		}
		Bee bee = bees [curBee].GetComponent<Bee> ();
		bool isKilled = bee.TakeDamage (damage);
		UpdateUI ();
		return isKilled;
	}

	public void UpdateUI() {
		// Change order of pictures
		// Change order of health
		for (int i = 0; i < numBees; i++) {
			if (bees[i] == null) {
				beeNames [i].text = "";
				beeHealths [i].text = "";
				beeImages [i].sprite = blankSprite;
				continue;
			}
			Bee b = bees [i].GetComponent<Bee> ();
			float curHP = b.GetCurHealth ();
			float maxHP = b.GetMaxHealth ();
			beeHealths [i].text = curHP.ToString () + "/" + maxHP.ToString ();
			beeNames [i].text = b.GetName ();
			beeImages [i].sprite = bees [i].GetComponent<SpriteRenderer> ().sprite;
			if (i == curBee) {
				GameObject.Find ("Bee" + i.ToString() + "Icon").GetComponent<RectTransform> ().sizeDelta = (new Vector2 (75, 75));
			}
			else {
				GameObject.Find ("Bee" + i.ToString() + "Icon").GetComponent<RectTransform> ().sizeDelta = (new Vector2 (50, 50));
			}
		}
	}

	void Update() {
		ReadSwap(); // Read in fixed update because of how keys are detected frame by frame
	}

	// Swaps bees based on input
	public void ReadSwap() {
		if (WasKeyPressed("1") && curBee != 0) {
			SwapBees (0);
		}
		else if (WasKeyPressed("2") && curBee != 1) {
			SwapBees (1);
		}
		else if (WasKeyPressed("3") && curBee != 2) {
			SwapBees (2);
		}
	}

	public void DeadBee(int deadNum) {
		for (int i = 0; i < numBees; i++) {
			if (i == deadNum) {
				continue;
			}
			if (bees[i] != null) {
				SwapBees (i);
				bees [deadNum] = null;
				UpdateUI ();
				return;
			}
		}
		SceneManager.LoadScene ("Loss"); // No bees left
	}

	public void GatherBee(GameObject newBee, GameObject oldBee) {
		for (int i = 0; i < numBees; i++) {
			if (bees[i] == null) {
				// Found empty spot to put a bee
				newBee.GetComponent<Bee> ().SetBeeNum (i);
				newBee.GetComponent<Bee> ().isInParty = true;
				bees [i] = newBee;
				UpdateUI ();
				return;
			}
		}
		// Replace the current bee
		newBee.GetComponent<Bee> ().SetBeeNum (curBee);
		newBee.GetComponent<Bee> ().isInParty = true;
		ParticleSystem ps = newBee.GetComponent<ParticleSystem> ();
		if (ps != null) {
			// If there is a particle system, disable it
			ParticleSystem.EmissionModule emissionMod = ps.emission;
			emissionMod.enabled = false;
		
		}
		oldBee.GetComponent<Rigidbody2D> ().velocity = (new Vector2 (0, 2));
		oldBee.GetComponent<BoxCollider2D> ().enabled = false;
		oldBee.GetComponent<Bee> ().enabled = false; // Disable the old bee
		newBee.GetComponent<BoxCollider2D> ().isTrigger = false;
		bees [curBee] = newBee;
		UpdateUI ();
		oldBee.GetComponent<Bee> ().isInParty = false;
	}

	void SwapBees(int swapTo) {
		// Do not swap if dead
		if (bees[swapTo] == null) {
			return;
		}
		Vector2 playerPosition = bees [curBee].transform.position;
		bees [curBee].transform.position = bees [swapTo].transform.position;
		bees [swapTo].transform.position = playerPosition;
		bees [curBee].GetComponent<Bee> ().DeactivateBee ();
		BeeManager.beeManager.curBee = swapTo;
		bees [curBee].GetComponent<Bee> ().ActivateBee ();

		UpdateUI ();
	}


	bool WasKeyPressed(string keyString) {
		return Input.GetKey(keyString);
	}

	public void StartKillerBee(GameObject killerBee) {
		killerBee.GetComponent<Bee> ().isInParty = true;
		bees [curBee].GetComponent<Bee> ().DeactivateBee ();
		bees.Add (killerBee);
		curBee = 4;
		numBees++;
		killerBee.GetComponent<BoxCollider2D> ().isTrigger = false;
		StartCoroutine (KillerBee(killerBee));

	}

	IEnumerator KillerBee(GameObject killerBee) {
		yield return new WaitForSeconds (10f);
		EndKillerBee (killerBee);
	}

	void EndKillerBee(GameObject killerBee) {
		killerBee.GetComponent<Bee> ().isInParty = false;
		numBees--;
		bees.Remove (killerBee);
		bees [0].transform.position = killerBee.transform.position;
		bees [0].GetComponent<Bee> ().ActivateBee ();
		killerBee.GetComponent<BoxCollider2D> ().enabled = false;
		curBee = 0;
	}
}
