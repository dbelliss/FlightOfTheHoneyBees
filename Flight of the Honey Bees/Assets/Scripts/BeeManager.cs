using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeManager : MonoBehaviour {
	public static int curBee = 0;
	public static GameObject[] bees;
	public static int numBees = 3;

	public static Text[] beeNames;
	public static Image[] beeImages;
	public static Text[] beeHealths;

	void Awake() {
		bees = new GameObject[numBees];
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

	public static bool TakeDamage(float damage) {
		Bee bee = bees [curBee].GetComponent<Bee> ();
		bool isKilled = bee.TakeDamage (damage);
		UpdateUI ();
		return isKilled;
	}

	public static void UpdateUI() {
		// Change order of pictures
		// Change order of health
		for (int i = 0; i < numBees; i++) {
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

	// Swaps bees based on input
	public static void ReadSwap() {
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

	static void SwapBees(int swapTo) {
		Vector2 playerPosition = bees [curBee].transform.position;
		bees [curBee].transform.position = bees [swapTo].transform.position;
		bees [swapTo].transform.position = playerPosition;
		bees [curBee].GetComponent<Bee> ().DeactivateBee ();
		BeeManager.curBee = swapTo;
		bees [curBee].GetComponent<Bee> ().ActivateBee ();

		UpdateUI ();
	}


	static bool WasKeyPressed(string keyString) {
		return Input.GetKey(keyString);
	}


}
