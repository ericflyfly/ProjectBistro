using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMScript : MonoBehaviour {

	public string appendString;
	public Text text01;

	// Use this for initialization
	void Start () {
		text01.text = appendString;
	}
	
	// Update is called once per frame
	void Update () {
		if (text01 != null) {
			text01.text = appendString + "\n" + (GameManager.allocatedTiles - GameManager.chosenTiles);
		}
	}
}
