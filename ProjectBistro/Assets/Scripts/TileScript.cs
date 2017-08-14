using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	private bool chosen = false;
	private Material mat;
	private Color baseColor;
	private GameObject model;
	private GameObject currentModel;
	private float yOffset;

	public Color highlightColor;
	public Color chosenColor;

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer>().material;
		baseColor = mat.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		if (!chosen) {
			mat.color = highlightColor;
		}
	}

	void OnMouseExit(){
		if (!chosen) {
			mat.color = baseColor;
		}
	}

	public bool ChooseTile(){
		if (!chosen) {
			mat.color = chosenColor;
			chosen = true;
			return true;
		} else {
			//Debug.Log ("Can't choose any more tiles!");
			return false;
		}
	}

	public bool UnchooseTile(){
		if (chosen) {
			mat.color = baseColor;
			chosen = false;
			if (currentModel != null) {
				Destroy (currentModel);
			}
			return true;
		} else {
			return false;
		}
	}

	//Set the item chosen to be displayed on this tile
	//TODO: use different int values to represent different items in parantheses
	public void SetItem(){
		if (chosen) {
			currentModel = Instantiate (this.model, new Vector3 (transform.position.x, yOffset, transform.position.z), model.transform.rotation);
			currentModel.transform.parent = this.transform;
		} else {
			Debug.Log ("Tile has not been chosen");
		}
	}

	public void SetModel(GameObject g){
		this.model = g;
	}

	public void SetYOffset(float yOff){
		this.yOffset = yOff;
	}
}
