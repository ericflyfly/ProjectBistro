using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	private Vector3 initialPos;

	public GameObject model;

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDrag(){
		transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, transform.position.y , Camera.main.ScreenToWorldPoint (Input.mousePosition).z);
	}

	void OnMouseUp(){
		transform.position = initialPos;

		if (GameManager.mouseOver.x > 0) {
			TileScript currentTile = GameManager.mapArray [(int)GameManager.mouseOver.x, (int)GameManager.mouseOver.y];
			currentTile.SetYOffset (0.4f);
			currentTile.SetModel (this.model);
			currentTile.SetItem ();
		}
	}
}
