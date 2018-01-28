using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public RectTransform rect;

	void Start() {
		
	}

	public void Highlight() {
		rect.sizeDelta = new Vector2 (200, 200);
	}

	public void UnHighlight() {
		rect.sizeDelta = new Vector2 (100, 100);
	}

}