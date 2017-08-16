using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	private Vector3 initialPos;
	private Vector3 initialScale;
	private Quaternion initialRot;

	public GameObject model;
	public Vector3 rotation;
	public int sizeMultiplier;
	public enum itemType {table, chair, waiter, blank}; 

	// Use this for initialization
	void Start () {
		initialPos = transform.position;
		initialScale = transform.localScale;
		initialRot = transform.rotation;

		transform.localScale *= sizeMultiplier;
		transform.rotation = Quaternion.Euler (rotation);


	}

	void OnMouseDrag(){
		transform.localScale = initialScale;
		transform.rotation = initialRot;
		transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, transform.position.y , Camera.main.ScreenToWorldPoint (Input.mousePosition).z);
	}

	void OnMouseUp(){
		transform.position = initialPos;
		transform.localScale *= sizeMultiplier;
		transform.rotation = Quaternion.Euler (rotation);

		if (GameManager.mouseOver.x > 0) {
			TileScript currentTile = GameManager.mapArray [(int)GameManager.mouseOver.x, (int)GameManager.mouseOver.y];
			currentTile.SetYOffset (0.4f);
			currentTile.SetModel (this.model);

			switch(gameObject.tag){
			case "Waiter":
				currentTile.SetItem (itemType.waiter);
				break;
			case "Table": 
				currentTile.SetItem (itemType.table);
				break;
			case "Chair":
				currentTile.SetItem (itemType.chair);
				break;
			}

		}
	}
}
